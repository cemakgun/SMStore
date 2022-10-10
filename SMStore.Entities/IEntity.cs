using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMStore.Entities
{
    public interface IEntity // Zorlama özelliği vardır class ların arayüzü yani bu interface den kalıtım alan herkes bu int ID almalı.
    {
        public int Id { get; set; }
    }
}
