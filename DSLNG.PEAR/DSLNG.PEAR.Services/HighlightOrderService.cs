﻿

using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.HighlightOrder;
using DSLNG.PEAR.Services.Responses.HighlightOrder;
using System.Linq;
using System.Data.Entity;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DSLNG.PEAR.Services
{
    public class HighlightOrderService : BaseService, IHighlightOrderService
    {
        public HighlightOrderService(IDataContext dataContext) :base(dataContext) { 
        }

        public GetHighlightOrdersResponse GetHighlights(GetHighlightOrdersRequest request)
        {

            int totalRecords;
            var query = SortData(request.Search, request.SortingDictionary, out totalRecords);
            if (request.Take != -1)
            {
                query = query.Skip(request.Skip).Take(request.Take);
            }
            var highlights = query.ToList();

            var response = new GetHighlightOrdersResponse();
            response.HighlightOrders = highlights.MapTo<GetHighlightOrdersResponse.HighlightOrderResponse>();
            response.TotalRecords = totalRecords;

            return response;
        }

        private IEnumerable<SelectOption> SortData(string search, IDictionary<string, System.Data.SqlClient.SortOrder> sortingDictionary, out int totalRecords)
        {
            var exception = new string[] { "alert" };
            var data = DataContext.SelectOptions.Include(x => x.Group)
                .Where(x => x.Select.Name == "highlight-types" && !exception.Contains(x.Value));
            if (!string.IsNullOrEmpty(search) && !string.IsNullOrWhiteSpace(search))
            {
                data = data.Where(x => x.Text.Contains(search) || x.Value.Contains(search));
            }

            foreach (var sortOrder in sortingDictionary)
            {
                switch (sortOrder.Key)
                {
                    case "Text":
                        data = sortOrder.Value == SortOrder.Ascending
                                   ? data.OrderBy(x => x.Text)
                                   : data.OrderByDescending(x => x.Text);
                        break;
                    case "Order":
                        data = sortOrder.Value == SortOrder.Ascending
                                   ? data.OrderBy(x => x.Order)
                                   : data.OrderByDescending(x => x.Order);
                        break;
                }
            }
            totalRecords = data.Count();
            return data;
        }

        public SaveHighlightOrderResponse SaveHighlight(SaveHighlightOrderRequest request)
        {
            try
            {
                var selectOption = new SelectOption { Id = request.Id };
                DataContext.SelectOptions.Attach(selectOption);
                selectOption.Order = request.Order;
                DataContext.SaveChanges();
                return new SaveHighlightOrderResponse
                {
                    IsSuccess = true,
                    Message = "You have been successfully save highlight order"
                };
            }
            catch (InvalidOperationException e) {
                return new SaveHighlightOrderResponse
                {
                    IsSuccess = false,
                    Message = "An error occured, please contact the administrator for further information"
                };
            }
        }
    }
}
