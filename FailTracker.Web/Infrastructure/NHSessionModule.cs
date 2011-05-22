using System;
using System.Web;
using NHibernate;
using StructureMap;

namespace FailTracker.Web.Infrastructure
{
	public class NHSessionModule : IHttpModule
	{
		private ISession _session;

		public void Init(HttpApplication context)
		{
			context.BeginRequest += CreateSession;
			context.EndRequest += DestroySession;
		}

		private void DestroySession(object sender, EventArgs e)
		{
			_session.Flush();
			_session.Close();
		}

		private void CreateSession(object sender, EventArgs e)
		{
			_session = ObjectFactory.GetInstance<ISession>();
		}

		public void Dispose()
		{
			_session = null;
		}
	}
}