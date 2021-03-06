using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace DogWalkerAPI.Models
{
    public class Walker
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NeighborhoodId { get; set; }

        public Neighborhood Neighborhood { get; set; }

        public List<Walks> Walks { get; set; }

    }
}