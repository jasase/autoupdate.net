namespace AutoUpdate.Core.Implementation.UpdaterManagementServices.Configurations
{
    public interface IUpdaterCheckIntervalConfigurationVisitor<TReturn>
    {
        TReturn Handle(UpdaterOneTimeCheckIntervalConfiguration updaterOneTimeCheckIntervalConfiguration);
    }
}
