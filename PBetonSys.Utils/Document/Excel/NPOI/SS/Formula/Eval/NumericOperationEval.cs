/*
* Licensed to the Apache Software Foundation (ASF) Under one or more
* contributor license agreements.  See the NOTICE file distributed with
* this work for Additional information regarding copyright ownership.
* The ASF licenses this file to You Under the Apache License, Version 2.0
* (the "License"); you may not use this file except in compliance with
* the License.  You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed Under the License Is distributed on an "AS Is" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations Under the License.
*/
/*
 * Created on May 14, 2005
 *
 */
namespace PBetonSys.Utils.NPOI.SS.Formula.Eval
{

    /**
     * @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
     *
     */
    public abstract class NumericOperationEval : OperationEval
    {

        protected abstract ValueEvalToNumericXlator Xlator { get; }

        protected ValueEval SingleOperandEvaluate(Eval eval, int srcRow, short srcCol)
        {
            ValueEval retval;
            if (eval is AreaEval)
            {
                AreaEval ae = (AreaEval)eval;
                if (ae.Contains(srcRow, srcCol))
                { // circular ref!
                    retval = ErrorEval.CIRCULAR_REF_ERROR;
                }
                else if (ae.IsRow)
                {
                    if (ae.ContainsColumn(srcCol))
                    {
                        ValueEval ve = ae.GetValue(ae.FirstRow, srcCol);
                        ve = Xlator.AttemptXlateToNumeric(ve);
                        retval = Xlator.AttemptXlateToNumeric(ve);
                    }
                    else
                    {
                        retval = ErrorEval.VALUE_INVALID;
                    }
                }
                else if (ae.IsColumn)
                {
                    if (ae.ContainsRow(srcRow))
                    {
                        ValueEval ve = ae.GetValue(srcRow, ae.FirstColumn);
                        retval = Xlator.AttemptXlateToNumeric(ve);
                    }
                    else
                    {
                        retval = ErrorEval.VALUE_INVALID;
                    }
                }
                else
                {
                    retval = ErrorEval.VALUE_INVALID;
                }
            }
            else
            {
                retval = Xlator.AttemptXlateToNumeric((ValueEval)eval);
            }
            return retval;
        }
        public abstract int NumberOfOperands { get; }

        public abstract int Type { get; }

        public abstract Eval Evaluate(Eval[] evals, int srcCellRow, short srcCellCol);
    }
}