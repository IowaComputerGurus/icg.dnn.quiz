using DotNetNuke.Entities.Modules;

namespace ICG.Modules.DnnQuiz
{
    /// <summary>
    ///     Quiz Module Settings
    /// </summary>
    public partial class Settings : ModuleSettingsBase
    {
        /// <summary>
        ///     Loads the settings.
        /// </summary>
        public override void LoadSettings()
        {
            var value = Settings["ICG_DQuiz_DisableEdit"];
            if (value != null)
            {
                chkDisallowEditAfterFirstQuiz.Checked = value.Equals("Y");
            }
        }

        /// <summary>
        ///     Updates the settings.
        /// </summary>
        public override void UpdateSettings()
        {
            var controller = new ModuleController();
            controller.UpdateModuleSetting(ModuleId, "ICG_DQuiz_DisableEdit",
                                           chkDisallowEditAfterFirstQuiz.Checked ? "Y" : "N");
        }
    }
}