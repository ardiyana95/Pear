using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Web.ViewModels.MidtermFormulation;
using System;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Requests.MidtermFormulation;

namespace DSLNG.PEAR.Web.Controllers
{
    public class MidtermFormulationController : BaseController
    {
        private readonly IMidtermFormulationService _midtermFormulationService;
        public MidtermFormulationController(IMidtermFormulationService midtermFormulationService) {
            _midtermFormulationService = midtermFormulationService;
        }
        [HttpPost]
        public ActionResult AddStage(MidtermPhaseStageViewModel viewModel) {
            var resp = _midtermFormulationService.SaveStage(viewModel.MapTo<AddStageRequest>());
            return Json(resp);
        }

        [HttpPost]
        public ActionResult AddDefinition(MidtermStageDefinitionViewModel viewModel) {
            var resp = _midtermFormulationService.AddDefinition(viewModel.MapTo<AddDefinitionRequest>());
            return Json(resp);
        }
        [HttpPost]
        public ActionResult DeleteStage(int id) {
            return Json(_midtermFormulationService.DeleteStage(id));
        }
        [HttpPost]
        public ActionResult DeleteStageDesc(int id)
        {
            return Json(_midtermFormulationService.DeleteStageDesc(id));
        }
        [HttpPost]
        public ActionResult DeleteStageKey(int id)
        {
            return Json(_midtermFormulationService.DeleteStageKey(id));
        }

        public ActionResult GetStagesByPbId(int id) {
            return Json(_midtermFormulationService.GetStagesByPbId(id), JsonRequestBehavior.AllowGet);
        }
	}
}