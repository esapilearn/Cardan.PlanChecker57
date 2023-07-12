using ESAPIX.Constraints;
using System;
using VMS.TPS.Common.Model.API;

namespace ESAPX_StarterUI.ViewModels
{
    public class CTDateConstraint : IConstraint
    {
        public string Name => "CT Date Constraint";

        public string FullName => "CT < 60 days old";

        public ConstraintResult CanConstrain(PlanningItem pi)
        {
            var pqa = new PQAsserter(pi);
            return pqa.HasImage()
                    .CumulativeResult;
        }

        public ConstraintResult Constrain(PlanningItem pi)
        {
            var ctDate = pi.StructureSet.Image.CreationDateTime;
            return ConstrainDate(ctDate);
        }

        public ConstraintResult ConstrainDate(DateTime? ctDate)
        {
            var daysOld = (DateTime.Now - ctDate).Value.TotalDays;

            if (daysOld > 60)
            {
                return new ConstraintResult(this, ResultType.ACTION_LEVEL_3, $"CT is {daysOld} days old");
            }
            else
            {
                return new ConstraintResult(this, ResultType.PASSED, $"CT is {daysOld} days old");
            }
        }
    }
}