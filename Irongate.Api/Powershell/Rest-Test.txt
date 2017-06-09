#------------------------------------------------------------------------------
# CONSTANTS:

Set-Variable TIME_STAMP (CreateTimestamp) -Option ReadOnly -Force;
Set-Variable LOG_FILE ("RestApiTest_" + ($TIME_STAMP + ".log")) -Option ReadOnly -Force;

Set-Variable BASE_URL ("http://localhost:8080/api") -Option ReadOnly -Force;
Set-Variable CONTENT_TYPE ("application/json") -Option ReadOnly -Force;

#------------------------------------------------------------------------------

function CreateTimestamp
{
    $TimeInfo = New-Object System.Globalization.DateTimeFormatInfo;
    $Timestamp = Get-Date -Format $TimeInfo.SortableDateTimePattern;
    $Timestamp = $Timestamp.Replace(":", "-");
    return $Timestamp;
}

function WriteToLog
{ param([string]$TextToWrite)
    
    $TextToWrite | Out-File $LOG_FILE -Append;
}

function LogErrorMessage
{ param([string]$ResultMsg, [string]$ErrorMsg)

    WriteToLog $ResultMsg;
    WriteToLog ("Error Message - " + $ErrorMsg);
    WriteToLog ""; 
}

function RunRoute_GET
{ param([string]$ApiRoute, [int]$SecondsAllowed)

    Write-Host "Testing $ApiRoute";
    $ResponseData = New-Object PSObject;

    Try
    {
        $ResponseData = (Invoke-RestMethod -Uri $ApiRoute -Method Get -DisableKeepAlive -TimeoutSec $SecondsAllowed);
        Write-Host "$ResponseData";
        #WriteToLog ($ResponseData);
    }
    Catch
    {
        Write-Host $_.Exception.Message;
    }

    return $ResponseData;
}

function RunRoute_POST
{ param([string]$ApiRoute, [object]$BodyContent, [int]$SecondsAllowed)

    Write-Host "Testing $ApiRoute";
    $ResponseData = New-Object PSObject;

    Try
    {
        $RequestHeader = @{};
        $RequestHeader.Add("Test", "Kiran");

        $ResponseData = (Invoke-RestMethod -Uri $ApiRoute -Headers $RequestHeader -Method Post -Body $BodyContent -ContentType $CONTENT_TYPE -DisableKeepAlive -TimeoutSec $SecondsAllowed);
        Write-Host "$RequestHeader";
        Write-Host "$ResponseData";
    }
    Catch
    {
           Write-Host $_.Exception.Response;
           Write-Host $_.Exception.Response.StatusDescription;
           Write-Host $_.Exception.Message;
    }

    return $ResponseData;
}


# Timer will measure total runtime of the testing process.
$Timer = [System.Diagnostics.Stopwatch]::StartNew();

# GET
$get = RunRoute_GET -ApiRoute ($BASE_URL + "/order") -SecondsAllowed 0;
$id = 1;
$getById = RunRoute_GET -ApiRoute ($BASE_URL + "/order/" + $id) -SecondsAllowed 0;


#POST
$payload = @{
    "value" = "test"; 
};
$ToDoRecord = RunRoute_POST -ApiRoute ($BASE_URL + "/order") -BodyContent (ConvertTo-Json $payload) -SecondsAllowed 0;



$Timer.Stop();
$RunTime = ("Test is complete. Total run time: " + $Timer.Elapsed.ToString())
Write-Host ($RunTime);
#WriteToLog $RunTime;
