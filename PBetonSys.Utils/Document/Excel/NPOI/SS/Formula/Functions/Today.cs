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
* distributed Under the License is distributed on an "AS Is" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations Under the License.
*/
/*
 * Created on May 15, 2005
 *
 */
namespace PBetonSys.Utils.NPOI.SS.Formula.Functions
{
    using System;
    using PBetonSys.Utils.NPOI.SS.Formula.Eval;
    

    public class Today : Fixed0ArgFunction
    {
        public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex)
        {
            return new NumberEval(PBetonSys.Utils.NPOI.SS.UserModel.DateUtil.GetExcelDate(DateTime.Today));
        }
    }
}
