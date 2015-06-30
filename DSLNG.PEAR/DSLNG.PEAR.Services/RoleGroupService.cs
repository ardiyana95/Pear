﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.RoleGroup;
using System;
using System.Linq;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Responses.RoleGroup;
using System.Collections.Generic;

namespace DSLNG.PEAR.Services
{
    public class RoleGroupService : BaseService, IRoleGroupService
    {
        public RoleGroupService (IDataContext DataContext) : base (DataContext){

        }

        public GetRoleGroupsResponse GetRoleGroups (GetRoleGroupsRequest request){
            var roleGroups = new List<RoleGroup>();
            if (request.Take != 0)
            {
                roleGroups = DataContext.RoleGroups.Include(x => x.Level).OrderBy(x => x.Id).Skip(request.Skip).Take(request.Take).ToList();
            }
            else
            {
                roleGroups = DataContext.RoleGroups.Include(x => x.Level).ToList();
            }
            var response = new GetRoleGroupsResponse();
            response.RoleGroups = roleGroups.MapTo<GetRoleGroupsResponse.RoleGroup>();
            return response;
        }

        public GetRoleGroupResponse GetRoleGroup (GetRoleGroupRequest request){
            try
            {
                var roleGroup = DataContext.RoleGroups.Include(x => x.Level).First(x => x.Id == request.Id);
                var response = roleGroup.MapTo<GetRoleGroupResponse>();

                return response;
            }
            catch (System.InvalidOperationException x)
            {
                return new GetRoleGroupResponse
                {
                    IsSuccess = false,
                    Message = x.Message
                };
            }
        }

        public CreateRoleGroupResponse Create(CreateRoleGroupRequest request)
        {
            var response = new CreateRoleGroupResponse();
            try
            {
                var roleGroup = request.MapTo<RoleGroup>();
                roleGroup.Level = DataContext.Levels.FirstOrDefault(x => x.Id == request.LevelId);
                DataContext.RoleGroups.Add(roleGroup);
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "RoleGroup type item has been added successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public UpdateRoleGroupResponse Update(UpdateRoleGroupRequest request)
        {
            var response = new UpdateRoleGroupResponse();
            try
            {
                var roleGroup = request.MapTo<RoleGroup>();
                roleGroup.Level = DataContext.Levels.FirstOrDefault(x => x.Id == request.LevelId);
                DataContext.RoleGroups.Attach(roleGroup);
                DataContext.Entry(roleGroup).State = EntityState.Modified;
                DataContext.SaveChanges();

                response.IsSuccess = true;
                response.Message = "User RoleGroup item has been updated successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public DeleteRoleGroupResponse Delete(int id)
        {
            var response = new DeleteRoleGroupResponse();
            try
            {
                var roleGroup = new Data.Entities.RoleGroup { Id = id };
                DataContext.RoleGroups.Attach(roleGroup);
                DataContext.Entry(roleGroup).State = EntityState.Deleted;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "User RoleGroup item has been deleted successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }
    }
}
