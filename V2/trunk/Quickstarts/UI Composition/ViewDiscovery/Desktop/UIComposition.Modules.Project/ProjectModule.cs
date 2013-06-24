//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using UIComposition.Modules.Project.Services;

namespace UIComposition.Modules.Project
{
    public class ProjectModule : IModule
    {
        private readonly IUnityContainer container;
        private readonly IRegionViewRegistry regionViewRegistry;

        public ProjectModule(IUnityContainer container, IRegionViewRegistry regionViewRegistry)
        {
            this.container = container;
            this.regionViewRegistry = regionViewRegistry;
        }

        public void Initialize()
        {
            this.RegisterViewsAndServices();

            // Register a type for pull based based composition. 
            this.regionViewRegistry.RegisterViewWithRegion("TabRegion",
                                                       () => this.container.Resolve<IProjectsListPresenter>().View);
        }

        protected void RegisterViewsAndServices()
        {
            this.container.RegisterType<IProjectService, ProjectService>(new ContainerControlledLifetimeManager());

            this.container.RegisterType<IProjectsListView, ProjectsListView>();
            this.container.RegisterType<IProjectsListPresenter, ProjectsListPresenter>();
        }
    }
}

