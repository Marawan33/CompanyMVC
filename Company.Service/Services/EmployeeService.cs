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
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Employee employee)
        {
            _unitOfWork.employeeRepository.Add(employee);
            _unitOfWork.Complete();
        }

        public void Delete(Employee employee)
        {
            _unitOfWork.employeeRepository.Delete(employee);
        }

        public IEnumerable<Employee> GetAll()
        {
            var emp = _unitOfWork.employeeRepository.GetAll();
            return emp;
        }

        public Employee GetById(int? id)
        {
            if (id is null)
            {
                return null;
            }
            var emp = _unitOfWork.employeeRepository.GetById(id.Value);
            if (emp is null)
            {
                return null;
            }
            return emp;
        }

        public IEnumerable<Employee> GetEmployeeByName(string name)
        {
           var emp =  _unitOfWork.employeeRepository.GetEmployeeByName(name);
            return emp;
        }

        public void Update(Employee employee)
        {
            _unitOfWork.employeeRepository.Update(employee);
            _unitOfWork.Complete();
        }
    }
}
