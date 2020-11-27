using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTO
{
    public class OptionDTO
    {
        public string Key { get; set; }
        public string Text { get; set; }
        public long Value { get; set; }
        public ImageDTO Image { get; set; }
    }

    public class ImageDTO
    {
        public bool Avatar { get; set; }
        public string Src { get; set; }
    }
}
