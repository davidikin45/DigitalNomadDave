using Owin;

namespace Solution.Base.Tasks
{
	public interface IRunAtOwinStartup
	{
		void Execute(IAppBuilder app, string nameOrConnectionString);
	}
}