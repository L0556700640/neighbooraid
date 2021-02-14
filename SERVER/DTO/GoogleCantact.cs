using System;

public class GoogleContacts
{
    public string version { get; set; }
    public string encoding { get; set; }
    public Feed feed { get; set; }
    public GoogleContacts()
    {

    }

    public GoogleContacts(string version, string encoding, Feed feed)
    {
        this.version = version;
        this.encoding = encoding;
        this.feed = feed;
    }
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
    public Feed()
    {

    }

    public Feed(string xmlns, string xmlnsopenSearch, string xmlnsbatch, string xmlnsgd, string xmlnsgContact, string gdetag, Id id, Updated updated, Category[] category, Title title, Link[] link, Author[] author, Generator generator, OpensearchTotalresults openSearchtotalResults, OpensearchStartindex openSearchstartIndex, OpensearchItemsperpage openSearchitemsPerPage, Entry[] entry)
    {
        this.xmlns = xmlns;
        this.xmlnsopenSearch = xmlnsopenSearch;
        this.xmlnsbatch = xmlnsbatch;
        this.xmlnsgd = xmlnsgd;
        this.xmlnsgContact = xmlnsgContact;
        this.gdetag = gdetag;
        this.id = id;
        this.updated = updated;
        this.category = category;
        this.title = title;
        this.link = link;
        this.author = author;
        this.generator = generator;
        this.openSearchtotalResults = openSearchtotalResults;
        this.openSearchstartIndex = openSearchstartIndex;
        this.openSearchitemsPerPage = openSearchitemsPerPage;
        this.entry = entry;
    }
}

public class Id
{
    public string t { get; set; }
    public Id()
    {

    }

    public Id(string t)
    {
        this.t = t;
    }
}

public class Updated
{
    public DateTime t { get; set; }
    public Updated()
    {

    }

    public Updated(DateTime t)
    {
        this.t = t;
    }
}

public class Title
{
    public string t { get; set; }
    public Title()
    {

    }

    public Title(string t)
    {
        this.t = t;
    }
}

public class Generator
{
    public string t { get; set; }
    public string uri { get; set; }
    public string version { get; set; }
    public Generator()
    {

    }

    public Generator(string t, string uri, string version)
    {
        this.t = t;
        this.uri = uri;
        this.version = version;
    }
}

public class OpensearchTotalresults
{
    public string t { get; set; }
    public OpensearchTotalresults()
    {

    }

    public OpensearchTotalresults(string t)
    {
        this.t = t;
    }
}

public class OpensearchStartindex
{
    public string t { get; set; }
    public OpensearchStartindex()
    {

    }

    public OpensearchStartindex(string t)
    {
        this.t = t;
    }
}

public class OpensearchItemsperpage
{
    public string t { get; set; }
    public OpensearchItemsperpage()
    {

    }

    public OpensearchItemsperpage(string t)
    {
        this.t = t;
    }
}

public class Category
{
    public string scheme { get; set; }
    public string term { get; set; }
    public Category()
    {

    }

    public Category(string scheme, string term)
    {
        this.scheme = scheme;
        this.term = term;
    }
}

public class Link
{
    public string rel { get; set; }
    public string type { get; set; }
    public string href { get; set; }
    public Link()
    {

    }

    public Link(string rel, string type, string href)
    {
        this.rel = rel;
        this.type = type;
        this.href = href;
    }
}

public class Author
{
    public Email email { get; set; }
    public Name name { get; set; }
    public Author()
    {

    }

    public Author(Email email, Name name)
    {
        this.email = email;
        this.name = name;
    }
}

public class Email
{
    public string t { get; set; }
    public Email()
    {

    }

    public Email(string t)
    {
        this.t = t;
    }
}

public class Name
{
    public string t { get; set; }
    public Name()
    {

    }

    public Name(string t)
    {
        this.t = t;
    }
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
    public Entry()
    {

    }

    public Entry(Id1 id, string gdetag, Updated1 updated, AppEdited appedited, Category1[] category, Title1 title, Link1[] link, GdEmail[] gdemail, GdName gdname, GdPhonenumber[] gdphoneNumber, GcontactGroupmembershipinfo[] gContactgroupMembershipInfo, GdOrganization[] gdorganization, GcontactWebsite[] gContactwebsite)
    {
        this.id = id;
        this.gdetag = gdetag;
        this.updated = updated;
        this.appedited = appedited;
        this.category = category;
        this.title = title;
        this.link = link;
        this.gdemail = gdemail;
        this.gdname = gdname;
        this.gdphoneNumber = gdphoneNumber;
        this.gContactgroupMembershipInfo = gContactgroupMembershipInfo;
        this.gdorganization = gdorganization;
        this.gContactwebsite = gContactwebsite;
    }
}

public class Id1
{
    public string t { get; set; }
    public Id1()
    {

    }

    public Id1(string t)
    {
        this.t = t;
    }
}

public class Updated1
{
    public DateTime t { get; set; }
    public Updated1()
    {

    }

    public Updated1(DateTime t)
    {
        this.t = t;
    }
}

public class AppEdited
{
    public string xmlnsapp { get; set; }
    public DateTime t { get; set; }
    public AppEdited()
    {

    }

    public AppEdited(string xmlnsapp, DateTime t)
    {
        this.xmlnsapp = xmlnsapp;
        this.t = t;
    }
}

public class Title1
{
    public string t { get; set; }
    public Title1()
    {

    }

    public Title1(string t)
    {
        this.t = t;
    }
}

public class GdName
{
    public GdFullname gdfullName { get; set; }
    public GdGivenname gdgivenName { get; set; }
    public GdFamilyname gdfamilyName { get; set; }
    public GdAdditionalname gdadditionalName { get; set; }
    public GdName()
    {

    }

    public GdName(GdFullname gdfullName, GdGivenname gdgivenName, GdFamilyname gdfamilyName, GdAdditionalname gdadditionalName)
    {
        this.gdfullName = gdfullName;
        this.gdgivenName = gdgivenName;
        this.gdfamilyName = gdfamilyName;
        this.gdadditionalName = gdadditionalName;
    }
}

public class GdFullname
{
    public string t { get; set; }
    public GdFullname()
    {

    }

    public GdFullname(string t)
    {
        this.t = t;
    }
}

public class GdGivenname
{
    public string t { get; set; }
    public GdGivenname()
    {

    }

    public GdGivenname(string t)
    {
        this.t = t;
    }
}

public class GdFamilyname
{
    public string t { get; set; }
    public GdFamilyname()
    {

    }

    public GdFamilyname(string t)
    {
        this.t = t;
    }
}

public class GdAdditionalname
{
    public string t { get; set; }
    public GdAdditionalname()
    {

    }

    public GdAdditionalname(string t)
    {
        this.t = t;
    }
}

public class Category1
{
    public string scheme { get; set; }
    public string term { get; set; }
    public Category1()
    {

    }

    public Category1(string scheme, string term)
    {
        this.scheme = scheme;
        this.term = term;
    }
}

public class Link1
{
    public string rel { get; set; }
    public string type { get; set; }
    public string href { get; set; }
    public string gdetag { get; set; }
    public Link1()
    {

    }

    public Link1(string rel, string type, string href, string gdetag)
    {
        this.rel = rel;
        this.type = type;
        this.href = href;
        this.gdetag = gdetag;
    }
}

public class GdEmail
{
    public string address { get; set; }
    public string primary { get; set; }
    public string rel { get; set; }
    public string label { get; set; }
    public GdEmail()
    {

    }

    public GdEmail(string address, string primary, string rel, string label)
    {
        this.address = address;
        this.primary = primary;
        this.rel = rel;
        this.label = label;
    }
}

public class GdPhonenumber
{
    public string label { get; set; }
    public string uri { get; set; }
    public string t { get; set; }
    public string rel { get; set; }
    public GdPhonenumber()
    {

    }

    public GdPhonenumber(string label, string uri, string t, string rel)
    {
        this.label = label;
        this.uri = uri;
        this.t = t;
        this.rel = rel;
    }
}

public class GcontactGroupmembershipinfo
{
    public string deleted { get; set; }
    public string href { get; set; }
    public GcontactGroupmembershipinfo()
    {

    }

    public GcontactGroupmembershipinfo(string deleted, string href)
    {
        this.deleted = deleted;
        this.href = href;
    }
}

public class GdOrganization
{
    public string rel { get; set; }
    public GdOrgname gdorgName { get; set; }
    public GdOrgtitle gdorgTitle { get; set; }
    public GdOrgdepartment gdorgDepartment { get; set; }
    public GdOrganization()
    {

    }

    public GdOrganization(string rel, GdOrgname gdorgName, GdOrgtitle gdorgTitle, GdOrgdepartment gdorgDepartment)
    {
        this.rel = rel;
        this.gdorgName = gdorgName;
        this.gdorgTitle = gdorgTitle;
        this.gdorgDepartment = gdorgDepartment;
    }
}

public class GdOrgname
{
    public string t { get; set; }
    public GdOrgname()
    {

    }

    public GdOrgname(string t)
    {
        this.t = t;
    }
}

public class GdOrgtitle
{
    public string t { get; set; }
    public GdOrgtitle()
    {

    }

    public GdOrgtitle(string t)
    {
        this.t = t;
    }
}

public class GdOrgdepartment
{
    public string t { get; set; }

    public GdOrgdepartment(string t)
    {
        this.t = t;
    }
    public GdOrgdepartment()
    {

    }
}

public class GcontactWebsite
{
    public string href { get; set; }
    public string rel { get; set; }

    public GcontactWebsite(string href, string rel)
    {
        this.href = href;
        this.rel = rel;
    }

    public GcontactWebsite()
    {
    }
}
