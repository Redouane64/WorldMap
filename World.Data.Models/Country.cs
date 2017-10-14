using System;
using System.Collections.Generic;
using System.Text;

namespace World.Data.Models
{
	public class Country
    {
	    public long Id { get; set; }
	    public string Name { get; set; }
	    public string Key { get; set; }
	    public string Code { get; set; }
	    public long Population { get; set; }
	    public long Area { get; set; }
    }
}
