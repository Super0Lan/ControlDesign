using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDesign.Models
{

    public class Rootobject
    {
        public string LastUpdateTime { get; set; }
        public Chinatotal ChinaTotal { get; set; }
        public Chinaadd ChinaAdd { get; set; }
        public bool IsShowAdd { get; set; }
        public Showaddswitch ShowAddSwitch { get; set; }
        public Areatree[] AreaTree { get; set; }
    }

    public class Chinatotal
    {
        public int Confirm { get; set; }
        public int Heal { get; set; }
        public int Dead { get; set; }
        public int NowConfirm { get; set; }
        public int Suspect { get; set; }
        public int NowSevere { get; set; }
        public int ImportedCase { get; set; }
        public int NoInfect { get; set; }
    }

    public class Chinaadd
    {
        public int Confirm { get; set; }
        public int Heal { get; set; }
        public int Dead { get; set; }
        public int NowConfirm { get; set; }
        public int Suspect { get; set; }
        public int NowSevere { get; set; }
        public int ImportedCase { get; set; }
        public int NoInfect { get; set; }
    }

    public class Showaddswitch
    {
        public bool All { get; set; }
        public bool Confirm { get; set; }
        public bool Suspect { get; set; }
        public bool Dead { get; set; }
        public bool Heal { get; set; }
        public bool NowConfirm { get; set; }
        public bool NowSevere { get; set; }
        public bool ImportedCase { get; set; }
        public bool NoInfect { get; set; }
    }

    public class Areatree
    {
        public string Name { get; set; }
        public Today Today { get; set; }
        public Total Total { get; set; }
        public Child[] Children { get; set; }
    }

    public class Today
    {
        public int Confirm { get; set; }
        public bool IsUpdated { get; set; }
    }

    public class Total
    {
        public int NowConfirm { get; set; }
        public int Confirm { get; set; }
        public int Suspect { get; set; }
        public int Dead { get; set; }
        public string DeadRate { get; set; }
        public bool ShowRate { get; set; }
        public int Heal { get; set; }
        public string HealRate { get; set; }
        public bool ShowHeal { get; set; }
    }

    public class Child
    {
        public string Name { get; set; }
        public Today1 Today { get; set; }
        public Total1 Total { get; set; }
        public Child1[] Children { get; set; }
    }

    public class Today1
    {
        public int Confirm { get; set; }
        public int ConfirmCuts { get; set; }
        public bool IsUpdated { get; set; }
        public string Tip { get; set; }
    }

    public class Total1
    {
        public int NowConfirm { get; set; }
        public int Confirm { get; set; }
        public int Suspect { get; set; }
        public int Dead { get; set; }
        public string DeadRate { get; set; }
        public bool ShowRate { get; set; }
        public int Heal { get; set; }
        public string HealRate { get; set; }
        public bool ShowHeal { get; set; }
    }

    public class Child1
    {
        public string Name { get; set; }
        public Today2 Today { get; set; }
        public Total2 Total { get; set; }
    }

    public class Today2
    {
        public int Confirm { get; set; }
        public int ConfirmCuts { get; set; }
        public bool IsUpdated { get; set; }
    }

    public class Total2
    {
        public int NowConfirm { get; set; }
        public int Confirm { get; set; }
        public int Suspect { get; set; }
        public int Dead { get; set; }
        public string DeadRate { get; set; }
        public bool ShowRate { get; set; }
        public int Heal { get; set; }
        public string HealRate { get; set; }
        public bool ShowHeal { get; set; }
    }

    public class ResponseMsg{
        public int Ret { get; set; }
        public string Data { get; set; }
    }

}
