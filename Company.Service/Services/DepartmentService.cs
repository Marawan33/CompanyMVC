using Company.Data.Models;
using Company.Repository.Interfaces;
using Company.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
          _unitOfWork = unitOfWork;
        }
        public void Add(Department department)
        {
            var mappedDepartment = new Department
            {
                Code = department.Code,
                Name = department.Name,
                CreatedAt = DateTime.Now,
            };
            _unitOfWork.departmentRepository.Add(mappedDepartment);
            _unitOfWork.Complete();
        }

        public void Delete(Department department)
        {
           _unitOfWork.departmentRepository.Delete(department);
        }

        public IEnumerable<Department> GetAll()
        {
            var dept = _unitOfWork.departmentRepository.GetAll();
            return dept;
        }

        public Department GetById(int? id)
        {
            if(id is null)
            {
                return null;
            }
            var dept = _unitOfWork.departmentRepository.GetById(id.Value);
            if(dept is null)
            {
                return null;
            }
            return dept;
        }

        public void Update(Department department)
        {
            _unitOfWork.departmentRepository.Update(department);
            _unitOfWork.Complete();
        }
    }
}
