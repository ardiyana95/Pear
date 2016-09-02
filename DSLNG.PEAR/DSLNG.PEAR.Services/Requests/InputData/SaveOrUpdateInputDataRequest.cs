﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.InputData
{
    public class SaveOrUpdateInputDataRequest
    {
        public SaveOrUpdateInputDataRequest()
        {
            GroupInputs = new List<GroupInputData>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PeriodeType { get; set; }
        public int UpdatedById { get; set; }
        
        public int AccountabilityId { get; set; }
        public int Order { get; set; }
        public IList<GroupInputData> GroupInputs { get; set; }

        public class GroupInputData
        {
            public GroupInputData()
            {
                InputDataAndKpiOrders = new List<InputDataAndKpiOrder>();
            }

            public string Name { get; set; }
            public IList<InputDataAndKpiOrder> InputDataAndKpiOrders { get; set; }
            public int Order { get; set; }
        }

        public class InputDataAndKpiOrder
        {
            public int KpiId { get; set; }
            public string KpiName { get; set; }
            public int Order { get; set; }
        }
    }
}
