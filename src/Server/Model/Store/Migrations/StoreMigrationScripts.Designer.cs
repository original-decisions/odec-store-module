﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace odec.CP.Server.Model.Store.Migrations {
    /// <summary>
    ///    A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [DebuggerNonUserCode()]
    [CompilerGenerated()]
    public class StoreMigrationScripts {
        
        private static ResourceManager resourceMan;
        
        private static CultureInfo resourceCulture;
        
        internal StoreMigrationScripts() {
        }
        
        /// <summary>
        ///    Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static ResourceManager ResourceManager {
            get {
                if (ReferenceEquals(resourceMan, null)) {
                    ResourceManager temp = new ResourceManager("odec.CP.Server.Model.Store.Migrations.StoreMigrationScripts", typeof(StoreMigrationScripts).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///    Overrides the current thread's CurrentUICulture property for all
        ///    resource lookups using this strongly typed resource class.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to  begin tran
        ///IF  NOT EXISTS (SELECT * FROM sys.objects 
        ///	WHERE object_id = OBJECT_ID(N&apos;[dbo].[Categories]&apos;) AND type in (N&apos;U&apos;))
        ///begin
        ///CREATE TABLE [dbo].[Categories] (
        ///    [Id] [int] NOT NULL IDENTITY,
        ///    [Name] [nvarchar](255) NOT NULL,
        ///    [IsApproved] [bit] NOT NULL,
        ///    [Code] [nvarchar](128) NOT NULL,
        ///    [IsActive] [bit] NOT NULL,
        ///    [SortOrder] [int] NOT NULL,
        ///    [DateUpdated] [datetime] NOT NULL,
        ///    [DateCreated] [datetime] NOT NULL,
        ///    CONSTRAINT [PK_dbo.Categories] PRIMARY KEY ([Id [rest of string was truncated]&quot;;.
        /// </summary>
        public static string BlobStoreMigrations {
            get {
                return ResourceManager.GetString("BlobStoreMigrations", resourceCulture);
            }
        }
    }
}
