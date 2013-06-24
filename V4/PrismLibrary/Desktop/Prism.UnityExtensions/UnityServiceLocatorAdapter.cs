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
using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Microsoft.Practices.Prism.UnityExtensions
{
    /// <summary>
    /// Defines a <seealso cref="IUnityContainer"/> adapter for the <see cref="IServiceLocator"/> interface to be used by the Composite Application Library.
    /// </summary>
    public class UnityServiceLocatorAdapter : ServiceLocatorImplBase
    {
        private readonly IUnityContainer _unityContainer;

        /// <summary>
        /// Initializes a new instance of <see cref="UnityServiceLocatorAdapter"/>.
        /// </summary>
        /// <param name="unityContainer">The <seealso cref="IUnityContainer"/> that will be used
        /// by the <see cref="DoGetInstance"/> and <see cref="DoGetAllInstances"/> methods.</param>
        [CLSCompliant(false)]
        public UnityServiceLocatorAdapter(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        /// <summary>
        /// Resolves the instance of the requested service.
        /// </summary>
        /// <param name="serviceType">Type of instance requested.</param>
        /// <param name="key">Name of registered service you want. May be null.</param>
        /// <returns>The requested service instance.</returns>
        protected override object DoGetInstance(Type serviceType, string key)
        {
            return _unityContainer.Resolve(serviceType, key);
        }

        /// <summary>
        /// Resolves all the instances of the requested service.
        /// </summary>
        /// <param name="serviceType">Type of service requested.</param>
        /// <returns>Sequence of service instance objects.</returns>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _unityContainer.ResolveAll(serviceType);
        }
    }
}