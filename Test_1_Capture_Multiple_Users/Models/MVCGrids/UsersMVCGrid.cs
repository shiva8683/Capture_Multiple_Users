using MVCGrid.Models;
using MVCGrid.Web;
using System.Linq;
using Test_1_Capture_Multiple_Users.Helpers;
using Test_1_Capture_Multiple_Users.Users;

namespace Test_1_Capture_Multiple_Users.Models.MVCGrids
{
    public class UsersMVCGrid
    {
        public const string USERS_LIST = nameof(USERS_LIST);
        public static void GetGridDefinition()
        {
            var colDefaults = new ColumnDefaults() { EnableSorting = true };
            MVCGridDefinitionTable.Add(USERS_LIST, new MVCGridBuilder<UserDTO>(colDefaults)
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .WithSorting(sorting: true, defaultSortColumn: nameof(UserDTO.FirstName), defaultSortDirection: SortDirection.Dsc)
                .WithPaging(paging: true, itemsPerPage: 15, allowChangePageSize: true, maxItemsPerPage: 100)
                .WithAdditionalQueryOptionNames("search")
                .AddColumns(cols =>
                {
                    cols.Add("Username").WithHeaderText("User Name").WithVisibility(true, true).WithValueExpression(p => p.FirstName).WithSorting(false);
                    cols.Add("FirstName").WithHeaderText("First Name").WithVisibility(true, true).WithValueExpression(p => p.LastName).WithSorting(false);
                    cols.Add("CellNumber").WithHeaderText("Cell Number").WithVisibility(true, true).WithValueExpression(p => p.Cellphone).WithSorting(false);
                    cols.Add("Actions").WithHtmlEncoding(false)
                        .WithSorting(false)
                        .WithHeaderText("Actions")
                        .WithValueExpression(x =>
                        {
                            return $"<a id='{Security.Encrypt(x.UserId)}' class='btn btn-success btn-sm' style='color: #fff;' onclick='EditUser(this)'> <i class='fa fa-edit' aria-hidden='true'></i>&nbsp;Edit</a>&nbsp;" +
                            $"<a id='{Security.Encrypt(x.UserId)}' class='btn btn-danger btn-sm ' style='color: #fff;' onclick='DeleteUser(this)'><i class='fa fa-trash' aria-hidden='true'></i>&nbsp; Delete</a>";
                        });
                })
                .WithAdditionalSetting(MVCGrid.Rendering.BootstrapRenderingEngine.SettingNameTableClass, "table table-striped table-bordered table-hover table-sm") // Example of changing table css class
                .WithRetrieveDataMethod((context) =>
                {
                    var options = context.QueryOptions;
                    var globalSearch = options.GetAdditionalQueryOptionString("search");
                    var sortColumn = options.GetSortColumnData<string>();
                    var userServiceClient = new UserServiceClient();
                    var usersList = userServiceClient.GetUsers().ToList();
                    if (options.GetLimitOffset().HasValue)
                        usersList = usersList.Skip(options.GetLimitOffset().Value).Take(options.GetLimitRowcount().Value).ToList();
                    return new QueryResult<UserDTO>()
                    {
                        Items = usersList,
                        TotalRecords = usersList.Count,
                    };
                })
            );
        }
    }
}