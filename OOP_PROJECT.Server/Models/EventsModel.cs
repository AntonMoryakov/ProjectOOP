﻿namespace OOP_PROJECT.Server.Models
{
    public class EventsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public string Picture { get; set; } // Новое свойство для хранения пути к изображению

    }
}
