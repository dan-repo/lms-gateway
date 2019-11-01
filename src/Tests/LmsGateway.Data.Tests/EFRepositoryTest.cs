
using LmsGateway.Data;
using LmsGateway.Domain.Registrations;
using Microsoft.EntityFrameworkCore;
using LmsGateway.Tests;
using System.Threading.Tasks;
using System.Linq;
using Xunit;

namespace LmsGateway.Data.Tests
{
    public class EFRepositoryTest
    {
        private Core.Data.IRepository<Faculty> _repository;

        public EFRepositoryTest()
        {
            _repository = new EFRepository<Faculty>(Db.GetDataContext());
        }

        [Fact]
        public void AddShouldPesistObjectAndReturnPersistedObjectWithItsID()
        {
            Faculty faculty = new Faculty() { Name = "Mangement Sciences", Description = "Mangement sciences description" };

            //_repository.Delete(x => x.Id >= 0);
            Faculty newFaculty = _repository.Add(faculty);

            AddEntityHelper(faculty, newFaculty);
        }
        
        [Fact]
        public async Task AddAsyncShouldPesistObjectAndReturnPersistedObjectWithItsID()
        {
            Faculty faculty = new Faculty() { Name = "Mangement Sciences 2", Description = "Mangement sciences description 2" };

            //_repository.Delete(x => x.Id >= 0);
            Faculty newFaculty = await _repository.AddAsync(faculty);

            AddEntityHelper(faculty, newFaculty);
        }
        
        private void AddEntityHelper(Faculty faculty, Faculty newFaculty)
        {
            Assert.NotNull(newFaculty);
            Assert.True(newFaculty.Id > 0);
            Assert.Equal(faculty.Name, newFaculty.Name);
            Assert.Equal(faculty.Description, newFaculty.Description);
        }




    }
}
