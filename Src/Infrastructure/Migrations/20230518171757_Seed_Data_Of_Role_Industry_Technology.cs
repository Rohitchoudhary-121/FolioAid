using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Data_Of_Role_Industry_Technology : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "AspNetRole",
               columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
               values: new object[] { "23643dfe-c611-42d4-9acf-b88905c4d88f", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
               table: "Industries",
               columns: new[] { "Id", "Name" },
               values: new object[,]
               {
                    { "0f6865ae-dc90-44e7-b8f0-60aed17b463f", "Automotive" },
                    { "1bd8bc2d-ee46-4042-a40e-a7c50ef41807", "Finance and Banking" },
                    { "27374726-3264-4c05-9302-3ef0c6c91581", "E-commerce" },
                    { "2831a0f6-98f1-49d7-9d6e-62ae51956356", "Entertainment and Media" },
                    { "2b219e07-9408-419d-9941-02d1b1121e65", "Energy and Utilities" },
                    { "2b5355c5-c2bf-461b-b043-b2d9ab836234", "Transportation and Logistics" },
                    { "365800d3-add4-4bd3-b465-c6729a64e928", "Retail" },
                    { "369a4186-9b42-4384-bf5a-565ad0b9a297", "Healthcare" },
                    { "37cfc654-6a6b-478f-bfcb-e42aff2156fc", "Agriculture" },
                    { "4b397745-4ab2-42be-b346-216f176e9502", "Government and Public Sector" },
                    { "6176391c-2f95-4faf-a3db-ee7c18244622", "Hospitality and Tourism" },
                    { "95216a12-3931-40bd-944c-a094eb582ee8", "Manufacturing" },
                    { "9f13c06e-308f-4411-9d90-e4931c60cefe", "Education" },
                    { "a1222d1e-5a70-4fc8-8535-a17a96cac235", "Real Estate" },
                    { "a6030290-d1a8-49da-b265-aae90e3729c9", "Construction" },
                    { "c77549ca-fea5-4ba6-bdd8-93c8bcdf3606", "Pharmaceuticals" },
                    { "c906c1d0-79fc-4c3c-931b-67e3d78826c6", "Telecommunications" },
                    { "cb8d5557-3e27-4c73-9cd6-36c8f63604cb", "Non-profit and Social Services" },
                    { "d4f44f08-97c0-4d7b-a153-66d1b511411d", "Sports and Fitness" },
                    { "fcd02fe2-8880-4b56-84e7-dccd3ac9df63", "Information Technology" }
               });

            migrationBuilder.InsertData(
               table: "Technologies",
               columns: new[] { "Id", "Name" },
               values: new object[,]
               {
                    { "1b21746a-c5b8-4e52-96f3-ee158481bba8", "LESS" },
                    { "326a8ad4-9e87-4ad4-8faa-3a369ff90b1b", "SASS" },
                    { "38c49f83-da88-4942-965f-864b9abece26", "JQuery" },
                    { "463d348d-9d2f-4e25-9ef8-9f82f54453ab", "ASP.NET Core" },
                    { "4be42909-9b1e-4291-b1d2-47495c2196e3", "React Native" },
                    { "524cdbc5-b6e0-46b7-b6a0-1d659d21f5a6", "AngularJS" },
                    { "6096c016-ab08-49fa-9964-20175142b194", "Angular" },
                    { "64c6b6ee-3037-4803-9d49-322044f1ff31", "KendoUI" },
                    { "683404f7-0836-4f97-a78a-7164603d70f6", ".NET Framework" },
                    { "7cdec8b1-23c8-4370-b68d-88e694a9117e", "VueJS" },
                    { "85ae78e4-de93-4d77-b5d7-7234d8dfee17", "HTML5" },
                    { "da5eea53-5639-42d2-8178-1555a54d34bc", "CSS" },
                    { "e8bcd572-95f1-4ff3-9d78-dd84ec3bea86", "ReactJS" }
               });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
               table: "AspNetRole",
               keyColumn: "Id",
               keyValue: "23643dfe-c611-42d4-9acf-b88905c4d88f");

            migrationBuilder.DeleteData(
                table: "Industries",
                keyColumn: "Id",
                keyValues: new object[]
                {
            "0f6865ae-dc90-44e7-b8f0-60aed17b463f",
            "1bd8bc2d-ee46-4042-a40e-a7c50ef41807",
            "27374726-3264-4c05-9302-3ef0c6c91581",
            "2831a0f6-98f1-49d7-9d6e-62ae51956356",
            "2b219e07-9408-419d-9941-02d1b1121e65",
            "2b5355c5-c2bf-461b-b043-b2d9ab836234",
            "365800d3-add4-4bd3-b465-c6729a64e928",
            "369a4186-9b42-4384-bf5a-565ad0b9a297",
            "37cfc654-6a6b-478f-bfcb-e42aff2156fc",
            "4b397745-4ab2-42be-b346-216f176e9502",
            "6176391c-2f95-4faf-a3db-ee7c18244622",
            "95216a12-3931-40bd-944c-a094eb582ee8",
            "9f13c06e-308f-4411-9d90-e4931c60cefe",
            "a1222d1e-5a70-4fc8-8535-a17a96cac235",
            "a6030290-d1a8-49da-b265-aae90e3729c9",
            "c77549ca-fea5-4ba6-bdd8-93c8bcdf3606",
            "c906c1d0-79fc-4c3c-931b-67e3d78826c6",
            "cb8d5557-3e27-4c73-9cd6-36c8f63604cb",
            "d4f44f08-97c0-4d7b-a153-66d1b511411d",
            "fcd02fe2-8880-4b56-84e7-dccd3ac9df63"
                });

            migrationBuilder.DeleteData(
                table: "Technologies",
                keyColumn: "Id",
                keyValues: new object[]
                {
            "1b21746a-c5b8-4e52-96f3-ee158481bba8",
            "326a8ad4-9e87-4ad4-8faa-3a369ff90b1b",
            "38c49f83-da88-4942-965f-864b9abece26",
            "463d348d-9d2f-4e25-9ef8-9f82f54453ab",
            "4be42909-9b1e-4291-b1d2-47495c2196e3",
            "524cdbc5-b6e0-46b7-b6a0-1d659d21f5a6",
            "6096c016-ab08-49fa-9964-20175142b194",
            "64c6b6ee-3037-4803-9d49-322044f1ff31",
            "683404f7-0836-4f97-a78a-7164603d70f6",
            "7cdec8b1-23c8-4370-b68d-88e694a9117e",
            "85ae78e4-de93-4d77-b5d7-7234d8dfee17",
            "da5eea53-5639-42d2-8178-1555a54d34bc",
            "e8bcd572-95f1-4ff3-9d78-dd84ec3bea86"
                });
        


    }
}
}
