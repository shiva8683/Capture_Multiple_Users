[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Test_1_Capture_Multiple_Users.MVCGridConfig), "RegisterGrids")]

namespace Test_1_Capture_Multiple_Users
{
    using Test_1_Capture_Multiple_Users.Models.MVCGrids;

    /// <summary>
    /// Registering all MVCGrids on pageload
    /// </summary>
    public static class MVCGridConfig 
    {
        public static void RegisterGrids()
        {
            //Registering the UserGrid
            UsersMVCGrid.GetGridDefinition();
        }
    }
}