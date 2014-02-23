//-----------------------------------------------------------------------
// <copyright file="IdentityModels.cs" company="Modena360">
//     Copyright (c) Modena360. All rights reserved.
// </copyright>
// <author>Agustín Cassani</author>
//-----------------------------------------------------------------------
namespace M360Engine.Model
{
    using Microsoft.AspNet.Identity.EntityFramework;

    /// <summary>
    /// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
    }

    /// <summary>
    /// Application DB Context
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext" /> class.
        /// </summary>
        public ApplicationDbContext()
            : base("M360EngineDBContext")
        {
        }
    }
}