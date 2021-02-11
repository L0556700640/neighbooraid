
using System;

public class Rootobject
{
    public string version { get; set; }
    public string encoding { get; set; }
    public Feed feed { get; set; }
}

public class Feed
{
    public string xmlns { get; set; }
    public string xmlnsopenSearch { get; set; }
    public string xmlnsbatch { get; set; }
    public string xmlnsgd { get; set; }
    public string xmlnsgContact { get; set; }
    public string gdetag { get; set; }
    public Id id { get; set; }
    public Updated updated { get; set; }
    public Category[] category { get; set; }
    public Title title { get; set; }
    public Link[] link { get; set; }
    public Author[] author { get; set; }
    public Generator generator { get; set; }
    public OpensearchTotalresults openSearchtotalResults { get; set; }
    public OpensearchStartindex openSearchstartIndex { get; set; }
    public OpensearchItemsperpage openSearchitemsPerPage { get; set; }
    public Entry[] entry { get; set; }
}

public class Id
{
    public string t { get; set; }
}

public class Updated
{
    public DateTime t { get; set; }
}

public class Title
{
    public string t { get; set; }
}

public class Generator
{
    public string t { get; set; }
    public string uri { get; set; }
    public string version { get; set; }
}

public class OpensearchTotalresults
{
    public string t { get; set; }
}

public class OpensearchStartindex
{
    public string t { get; set; }
}

public class OpensearchItemsperpage
{
    public string t { get; set; }
}

public class Category
{
    public string scheme { get; set; }
    public string term { get; set; }
}

public class Link
{
    public string rel { get; set; }
    public string type { get; set; }
    public string href { get; set; }
}

public class Author
{
    public Email email { get; set; }
    public Name name { get; set; }
}

public class Email
{
    public string t { get; set; }
}

public class Name
{
    public string t { get; set; }
}

public class Entry
{
    public Id1 id { get; set; }
    public string gdetag { get; set; }
    public Updated1 updated { get; set; }
    public AppEdited appedited { get; set; }
    public Category1[] category { get; set; }
    public Title1 title { get; set; }
    public Link1[] link { get; set; }
    public GdEmail[] gdemail { get; set; }
    public GdName gdname { get; set; }
    public GdPhonenumber[] gdphoneNumber { get; set; }
    public GcontactGroupmembershipinfo[] gContactgroupMembershipInfo { get; set; }
    public GdOrganization[] gdorganization { get; set; }
    public GcontactWebsite[] gContactwebsite { get; set; }
}

public class Id1
{
    public string t { get; set; }
}

public class Updated1
{
    public DateTime t { get; set; }
}

public class AppEdited
{
    public string xmlnsapp { get; set; }
    public DateTime t { get; set; }
}

public class Title1
{
    public string t { get; set; }
}

public class GdName
{
    public GdFullname gdfullName { get; set; }
    public GdGivenname gdgivenName { get; set; }
    public GdFamilyname gdfamilyName { get; set; }
    public GdAdditionalname gdadditionalName { get; set; }
}

public class GdFullname
{
    public string t { get; set; }
}

public class GdGivenname
{
    public string t { get; set; }
}

public class GdFamilyname
{
    public string t { get; set; }
}

public class GdAdditionalname
{
    public string t { get; set; }
}

public class Category1
{
    public string scheme { get; set; }
    public string term { get; set; }
}

public class Link1
{
    public string rel { get; set; }
    public string type { get; set; }
    public string href { get; set; }
    public string gdetag { get; set; }
}

public class GdEmail
{
    public string address { get; set; }
    public string primary { get; set; }
    public string rel { get; set; }
    public string label { get; set; }
}

public class GdPhonenumber
{
    public string label { get; set; }
    public string uri { get; set; }
    public string t { get; set; }
    public string rel { get; set; }
}

public class GcontactGroupmembershipinfo
{
    public string deleted { get; set; }
    public string href { get; set; }
}

public class GdOrganization
{
    public string rel { get; set; }
    public GdOrgname gdorgName { get; set; }
    public GdOrgtitle gdorgTitle { get; set; }
    public GdOrgdepartment gdorgDepartment { get; set; }
}

public class GdOrgname
{
    public string t { get; set; }
}

public class GdOrgtitle
{
    public string t { get; set; }
}

public class GdOrgdepartment
{
    public string t { get; set; }
}

public class GcontactWebsite
{
    public string href { get; set; }
    public string rel { get; set; }
}
