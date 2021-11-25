using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App_Library.Interfaces
{
    public interface IDatabaseSingleton
    {
        public void SetRepository(IRepository repository);
        public IRepository GetRepository();
        public void SetMoqRepository(Mock<IRepository> repository);
    }
}
