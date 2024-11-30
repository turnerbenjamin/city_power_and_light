#pragma warning disable CS1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: Microsoft.Xrm.Sdk.Client.ProxyTypesAssemblyAttribute()]

namespace CityPowerAndLight.Model
{
	
	
	/// <summary>
	/// Represents a source of entities bound to a Dataverse service. It tracks and manages changes made to the retrieved entities.
	/// </summary>
	public partial class OrgContext : Microsoft.Xrm.Sdk.Client.OrganizationServiceContext
	{
		
		/// <summary>
		/// Constructor.
		/// </summary>
		public OrgContext(Microsoft.Xrm.Sdk.IOrganizationService service) : 
				base(service)
		{
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="CityPowerAndLight.Model.Account"/> entities.
		/// </summary>
		public System.Linq.IQueryable<CityPowerAndLight.Model.Account> AccountSet
		{
			get
			{
				return this.CreateQuery<CityPowerAndLight.Model.Account>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="CityPowerAndLight.Model.Contact"/> entities.
		/// </summary>
		public System.Linq.IQueryable<CityPowerAndLight.Model.Contact> ContactSet
		{
			get
			{
				return this.CreateQuery<CityPowerAndLight.Model.Contact>();
			}
		}
		
		/// <summary>
		/// Gets a binding to the set of all <see cref="CityPowerAndLight.Model.Incident"/> entities.
		/// </summary>
		public System.Linq.IQueryable<CityPowerAndLight.Model.Incident> IncidentSet
		{
			get
			{
				return this.CreateQuery<CityPowerAndLight.Model.Incident>();
			}
		}
	}
}
#pragma warning restore CS1591
