using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3Core.Models
{
    public class Build
    {
        public string Name { get; set; }
        public string URL { get; set; }

        public HeroClass Class
        {
            get
            {
                if(Name.Contains("Barbarian"))
                    return HeroClass.Barbarian;
                if(Name.Contains("Crusader"))
                    return HeroClass.Crusader;
                if(Name.Contains("Hunter"))
                    return HeroClass.DemonHunter;
                if(Name.Contains("Monk"))
                    return HeroClass.Monk;
                if(Name.Contains("Necromancer"))
                    return HeroClass.Necromancer;
                if(Name.Contains("Doctor"))
                    return HeroClass.WitchDoctor;
                if(Name.Contains("Wizard"))
                    return HeroClass.Wizard;
                return HeroClass.Undefined;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
