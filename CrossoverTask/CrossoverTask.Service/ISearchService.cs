using CrossoverTask.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CrossoverTask.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ISearchService
    {
        [OperationContract]
        bool Search(SearchInput searchParams);        
    }

    [DataContract]
    public class SearchInput
    {
        [DataMember]
        public Gender? Gender { get; set; }

        [DataMember]
        public int? AgeStart { get; set; }

        [DataMember]
        public int? AgeEnd { get; set; }

        [DataMember]
        public float? HeightStart { get; set; }

        [DataMember]
        public float? HeightEnd { get; set; }

        [DataMember]
        public float? WeightStart { get; set; }

        [DataMember]
        public float? WeightEnd { get; set; }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public int MaxNumberOfResults { get; set; }

        [DataMember]
        public string RecieverEmail { get; set; }

        [DataMember]
        public string SecretKey { get; set; }

    }
}
