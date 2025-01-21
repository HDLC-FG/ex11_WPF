﻿using static WPF.MVVM.Enums;

namespace WPF.MVVM.Models
{
    public class Engine
    {
        public int Id { get; set; }
        public TypeEngine Type { get; set; }
        public int Horsepower { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return $"{Type}, {Horsepower} chevaux, {Price}€";
        }
    }
}
