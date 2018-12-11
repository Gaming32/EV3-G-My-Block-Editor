using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Display_EV3_G_My_Block.EV3P
{
    class MyBlock_Parameter
    {
        public enum Directions {Input = 0, Output = 1};
        public enum DataTypes {Numeric, Logic, Text, NumericArray, LogicArray}

        public string displayName { get; set; }
        public Directions direction { get; set; }
        public ushort imageId { get; set; }
        public DataTypes dataType { get; set; }

        public MyBlock_Parameter(string displayName, Directions direction, ushort imageId, DataTypes dataType)
        {
            this.displayName = displayName;
            this.direction = direction;
            this.imageId = imageId;
            this.dataType = dataType;
        }
        public MyBlock_Parameter(string displayName, string direction, string imageId, string dataType)
        {
            this.displayName = displayName;
            this.direction = ConvertDirectionTextToId(direction);
            this.imageId = ConvertImageTextToId(imageId);
            this.dataType = ConvertDataTypeTextToId(dataType);
        }

        public static Directions ConvertDirectionTextToId(string text)
        {
            if (text == "Output")
                return Directions.Output;
            else
                return Directions.Input;
        }
        public static ushort ConvertImageTextToId(string text)
        {
            text = text.TrimStart("Identification_".ToCharArray());
            text = text.Remove(3);
            return ushort.Parse(text);
        }
        public static DataTypes ConvertDataTypeTextToId(string text)
        {
            switch (text)
            {
                case "Boolean":
                    return DataTypes.Logic;
                case "String":
                    return DataTypes.Text;
                case "Single[]":
                    return DataTypes.NumericArray;
                case "Boolean[]":
                    return DataTypes.LogicArray;
                default:
                    return DataTypes.Numeric;
            }
        }
    }
}
