using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PBetonSys.Utils.EPPlus.DataValidation.Formulas.Contracts;

namespace PBetonSys.Utils.EPPlus.DataValidation.Contracts
{
    public interface IExcelDataValidationInt : IExcelDataValidationWithFormula2<IExcelDataValidationFormulaInt>, IExcelDataValidationWithOperator
    {
    }
}
