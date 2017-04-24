﻿using System;
using System.Collections.Generic;
using System.Text;
using PBetonSys.Utils.NPOI.Util;
using PBetonSys.Utils.NPOI.HSSF.Record;

using PBetonSys.Utils.NPOI.SS.Formula;

namespace PBetonSys.Utils.NPOI.SS.Util
{
    public class CellRangeAddress : CellRangeAddressBase
    {
        public static int ENCODED_SIZE = 8;

        public CellRangeAddress(int firstRow, int lastRow, int firstCol, int lastCol)
            : base(firstRow, lastRow, firstCol, lastCol)
        {

        }

        public CellRangeAddress(RecordInputStream in1)
            : base(ReadUShortAndCheck(in1), in1.ReadUShort(), in1.ReadUShort(), in1.ReadUShort())
        {

        }

        private static int ReadUShortAndCheck(RecordInputStream in1)
        {
            if (in1.Remaining < ENCODED_SIZE)
            {
                // Ran out of data
                throw new Exception("Ran out of data readin1g CellRangeAddress");
            }
            return in1.ReadUShort();
        }
        [Obsolete]
        public int Serialize(int offset, byte[] data)
        {
            LittleEndian.PutUShort(data, offset + 0, FirstRow);
            LittleEndian.PutUShort(data, offset + 2, LastRow);
            LittleEndian.PutUShort(data, offset + 4, FirstColumn);
            LittleEndian.PutUShort(data, offset + 6, LastColumn);
            return ENCODED_SIZE;
        }
        public void Serialize(ILittleEndianOutput out1)
        {
            out1.WriteShort(FirstRow);
            out1.WriteShort(LastRow);
            out1.WriteShort(FirstColumn);
            out1.WriteShort(LastColumn);
        }
        public String FormatAsString()
        {
            return FormatAsString(null, false);
        }
        /**
     * @return the text format of this range using specified sheet name.
     */
        public String FormatAsString(String sheetName, bool useAbsoluteAddress)
        {
            StringBuilder sb = new StringBuilder();
            if (sheetName != null)
            {
                sb.Append(SheetNameFormatter.Format(sheetName));
                sb.Append("!");
            }
            CellReference cellRefFrom = new CellReference(FirstRow, FirstColumn,
                    useAbsoluteAddress, useAbsoluteAddress);
            CellReference cellRefTo = new CellReference(LastRow, LastColumn,
                    useAbsoluteAddress, useAbsoluteAddress);
            sb.Append(cellRefFrom.FormatAsString());

            //for a single-cell reference return A1 instead of A1:A1
            if (!cellRefFrom.Equals(cellRefTo))
            {
                sb.Append(':');
                sb.Append(cellRefTo.FormatAsString());
            }
            return sb.ToString();
        }
        public CellRangeAddress Copy()
        {
            return new CellRangeAddress(FirstRow, LastRow, FirstColumn, LastColumn);
        }

        public static int GetEncodedSize(int numberOfItems)
        {
            return numberOfItems * ENCODED_SIZE;
        }

            /**
     * @param ref usually a standard area ref (e.g. "B1:D8").  May be a single cell
     *            ref (e.g. "B5") in which case the result is a 1 x 1 cell range.
     */
        public static CellRangeAddress ValueOf(String reference)
        {
            int sep = reference.IndexOf(":", StringComparison.Ordinal);
            CellReference a;
            CellReference b;
            if (sep == -1)
            {
                a = new CellReference(reference);
                b = a;
            }
            else
            {
                a = new CellReference(reference.Substring(0, sep));
                b = new CellReference(reference.Substring(sep + 1));
            }
            return new CellRangeAddress(a.Row, b.Row, a.Col, b.Col);
        }
    }
}
