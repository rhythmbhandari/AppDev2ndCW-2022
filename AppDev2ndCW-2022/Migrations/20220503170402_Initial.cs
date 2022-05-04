using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDev2ndCW_2022.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actor",
                columns: table => new
                {
                    ActorNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActorSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActorFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actor", x => x.ActorNumber);
                });

            migrationBuilder.CreateTable(
                name: "DvdCategory",
                columns: table => new
                {
                    CategoryNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeRestricted = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DvdCategory", x => x.CategoryNumber);
                });

            migrationBuilder.CreateTable(
                name: "LoanTypes",
                columns: table => new
                {
                    LoanTypeNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoanDuration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTypes", x => x.LoanTypeNumber);
                });

            migrationBuilder.CreateTable(
                name: "MembershipCategory",
                columns: table => new
                {
                    MembershipCategoryNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembershipCategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MembershipCategoryTotalLoans = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipCategory", x => x.MembershipCategoryNumber);
                });

            migrationBuilder.CreateTable(
                name: "Producer",
                columns: table => new
                {
                    ProducerNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProducerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producer", x => x.ProducerNumber);
                });

            migrationBuilder.CreateTable(
                name: "Studio",
                columns: table => new
                {
                    StudioNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudioName = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studio", x => x.StudioNumber);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserNumber);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MembershipCategoryNumber = table.Column<long>(type: "bigint", nullable: false),
                    MemberLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberDOB = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberNumber);
                    table.ForeignKey(
                        name: "FK_Member_MembershipCategory_MembershipCategoryNumber",
                        column: x => x.MembershipCategoryNumber,
                        principalTable: "MembershipCategory",
                        principalColumn: "MembershipCategoryNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DvdTitle",
                columns: table => new
                {
                    DvdNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProducerNumber = table.Column<long>(type: "bigint", nullable: false),
                    CategoryNumber = table.Column<long>(type: "bigint", nullable: false),
                    StudioNumber = table.Column<long>(type: "bigint", nullable: false),
                    DateReleased = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StandardCharge = table.Column<int>(type: "int", nullable: false),
                    PenaltyCharge = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DvdTitle", x => x.DvdNumber);
                    table.ForeignKey(
                        name: "FK_DvdTitle_DvdCategory_CategoryNumber",
                        column: x => x.CategoryNumber,
                        principalTable: "DvdCategory",
                        principalColumn: "CategoryNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DvdTitle_Producer_ProducerNumber",
                        column: x => x.ProducerNumber,
                        principalTable: "Producer",
                        principalColumn: "ProducerNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DvdTitle_Studio_StudioNumber",
                        column: x => x.StudioNumber,
                        principalTable: "Studio",
                        principalColumn: "StudioNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CastMember",
                columns: table => new
                {
                    DvdNumber = table.Column<long>(type: "bigint", nullable: false),
                    ActorNumber = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CastMember", x => new { x.DvdNumber, x.ActorNumber });
                    table.ForeignKey(
                        name: "FK_CastMember_Actor_ActorNumber",
                        column: x => x.ActorNumber,
                        principalTable: "Actor",
                        principalColumn: "ActorNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CastMember_DvdTitle_DvdNumber",
                        column: x => x.DvdNumber,
                        principalTable: "DvdTitle",
                        principalColumn: "DvdNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DvdCopy",
                columns: table => new
                {
                    CopyNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DvdNumber = table.Column<long>(type: "bigint", nullable: false),
                    DatePurchased = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DvdCopy", x => x.CopyNumber);
                    table.ForeignKey(
                        name: "FK_DvdCopy_DvdTitle_DvdNumber",
                        column: x => x.DvdNumber,
                        principalTable: "DvdTitle",
                        principalColumn: "DvdNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    LoanNumber = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanTypeNumber = table.Column<long>(type: "bigint", nullable: false),
                    CopyNumber = table.Column<long>(type: "bigint", nullable: false),
                    MemberNumber = table.Column<long>(type: "bigint", nullable: false),
                    DateOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateReturned = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loan", x => x.LoanNumber);
                    table.ForeignKey(
                        name: "FK_Loan_DvdCopy_CopyNumber",
                        column: x => x.CopyNumber,
                        principalTable: "DvdCopy",
                        principalColumn: "CopyNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loan_LoanTypes_LoanTypeNumber",
                        column: x => x.LoanTypeNumber,
                        principalTable: "LoanTypes",
                        principalColumn: "LoanTypeNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loan_Member_MemberNumber",
                        column: x => x.MemberNumber,
                        principalTable: "Member",
                        principalColumn: "MemberNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CastMember_ActorNumber",
                table: "CastMember",
                column: "ActorNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DvdCopy_DvdNumber",
                table: "DvdCopy",
                column: "DvdNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DvdTitle_CategoryNumber",
                table: "DvdTitle",
                column: "CategoryNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DvdTitle_ProducerNumber",
                table: "DvdTitle",
                column: "ProducerNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DvdTitle_StudioNumber",
                table: "DvdTitle",
                column: "StudioNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_CopyNumber",
                table: "Loan",
                column: "CopyNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_LoanTypeNumber",
                table: "Loan",
                column: "LoanTypeNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_MemberNumber",
                table: "Loan",
                column: "MemberNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Member_MembershipCategoryNumber",
                table: "Member",
                column: "MembershipCategoryNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CastMember");

            migrationBuilder.DropTable(
                name: "Loan");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Actor");

            migrationBuilder.DropTable(
                name: "DvdCopy");

            migrationBuilder.DropTable(
                name: "LoanTypes");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "DvdTitle");

            migrationBuilder.DropTable(
                name: "MembershipCategory");

            migrationBuilder.DropTable(
                name: "DvdCategory");

            migrationBuilder.DropTable(
                name: "Producer");

            migrationBuilder.DropTable(
                name: "Studio");
        }
    }
}
