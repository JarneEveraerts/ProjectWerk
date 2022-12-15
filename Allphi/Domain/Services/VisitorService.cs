using Domain.Models;
using Domain.Models.DTOs;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IVisitorRepository _visitorRepo;
        public VisitorService(IVisitorRepository visitorRepository)
        {
            _visitorRepo = visitorRepository;
        }

        public Task<List<Visitor>> GetVisitors()
        {
            return _visitorRepo.GetVisitors();
        }
        public Task<List<Visitor>> GetVisitorsByBusiness(string business)
        {
            return _visitorRepo.GetVisitorsByBusiness(business);
        }

        public Visitor GetVisitorById(int id)
        {
            return _visitorRepo.GetVisitorById(id);
        }
        public Visitor GetVisitorByMail(string email)
        {
            return _visitorRepo.GetVisitorByMail(email);
        }
        public Visitor GetVisitorByName(string name)
        {
            return _visitorRepo.GetVisitorByName(name);
        }

        public void UpdateVisitor(UpdateVisitorDTO updateVisitorDTO, int id)
        {
            Visitor visitor = _visitorRepo.GetVisitorById(id);
            visitor.Name = updateVisitorDTO.Name;
            visitor.Email = updateVisitorDTO.Email;
            visitor.Plate = updateVisitorDTO.Plate;
            visitor.Business = updateVisitorDTO.Business;
            _visitorRepo.UpdateVisitor(visitor);
        }
        public Visitor CreateVisitor(CreateVisitorDTO visitorDTO)
        {
            Visitor visitor = new Visitor
            {
                Name = visitorDTO.Name,
                Email = visitorDTO.Email,
                Business = visitorDTO.Organisation
            };
            _visitorRepo.CreateVisitor(visitor);
            return visitor;
        }
        public void CreateVisitorWithPlate(CreateVisitorWithPlateDTO visitorWithPlateDTO)
        {
            //parkingspot? parkingspot = _parkingrepo.getparkingspotbyplate(visitorplate);
            //visitor? visitor = new visitor(visitorname, visitoremail, organisation, visitorplate);
            //if (parkingspotexists(visitorplate))
            //{
            //    _visitorRepo.CreateVisitor(visitor);
            //    parkingSpot.Visitor = visitor;
            //    _parkingRepo.UpdateParkingSpot(parkingSpot);
            //    CreateVisit(visitor, employee, business);
            //}
        }

        public void CreateVisitorBalie(CreateVisitorBalieDTO visitorDTO)
        {
            _visitorRepo.CreateVisitor(new Visitor(
                visitorDTO.Name,
                visitorDTO.Email,
                visitorDTO.Business,
                visitorDTO.Plate));
        }

        public void DeleteVisitor(int id)
        {
            Visitor visitor = _visitorRepo.GetVisitorById(id);
            visitor.IsDeleted = true;
            _visitorRepo.UpdateVisitor(visitor);
        }



    }
}
