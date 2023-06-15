namespace IoDit.WebAPI.Utilities.Loriot.Types;

public class LoriotAppResponse
{
    public List<LoriotApp> apps { get; set; }
    public int total { get; set; }
    public int page { get; set; }
    public int perPage { get; set; }
}

public class AccessRight
{
    public string token { get; set; }
    public bool data { get; set; }
    public bool appServer { get; set; }
    public bool devProvisioning { get; set; }
}

public class LoriotApp
{
    public object _id { get; set; }
    public string appHexId { get; set; }
    public string name { get; set; }
    public int ownerid { get; set; }
    public int organizationId { get; set; }
    public string visibility { get; set; }
    public DateTime created { get; set; }
    public int devices { get; set; }
    public int deviceLimit { get; set; }
    public int mcastdevices { get; set; }
    public int mcastdevlimit { get; set; }
    public List<LoriotOutput> outputs { get; set; }
    public string overbosity { get; set; }
    public string odataenc { get; set; }
    public string ogwinfo { get; set; }
    public bool orx { get; set; }
    public bool cansend { get; set; }
    public bool canotaa { get; set; }
    public bool suspended { get; set; }
    public string masterkey { get; set; }
    public int clientsLimit { get; set; }
    public CfgDevBase cfgDevBase { get; set; }
    public bool publishAppSKey { get; set; }
    public List<AccessRight> accessRights { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        LoriotApp other = (LoriotApp) obj;
        return appHexId == other.appHexId; // Compare the properties that uniquely identify the app
    }

    public override int GetHashCode()
    {
        return appHexId.GetHashCode(); // Use the hash code of the property that uniquely identifies the app
    }
}

public class CfgDevBase
{
    public bool adr { get; set; }
    public object adrFix { get; set; }
    public object adrMax { get; set; }
    public object adrMin { get; set; }
    public string devclass { get; set; }
    public int dutycycle { get; set; }
    public int rxw { get; set; }
    public bool seqdnreset { get; set; }
    public bool seqrelax { get; set; }
}

public class LoriotOutput
{
    public int id { get; set; }
    public LoriotOsetup osetup { get; set; }
    public string output { get; set; }
}

public class LoriotOsetup
{
    public string name { get; set; }
    public string iothubname { get; set; }
    public string primarykey { get; set; }
}