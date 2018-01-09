using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irongate.Element.Mongo
{
    public interface IMongoRepository
    {
        Task<bool> SaveSomething(object fireStuff, string collection);
    }
}
