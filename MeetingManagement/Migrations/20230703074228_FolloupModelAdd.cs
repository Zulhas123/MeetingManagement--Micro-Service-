﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingManagement.Migrations
{
    /// <inheritdoc />
    public partial class FolloupModelAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "FollowUpMeeting",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<int>(type: "int", nullable: false),
            //        MettingId = table.Column<int>(type: "int", nullable: false),
            //        CreatedBy = table.Column<int>(type: "int", nullable: false),
            //        CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        IsActive = table.Column<bool>(type: "bit", nullable: false),
            //        UpdatedBy = table.Column<int>(type: "int", nullable: true),
            //        UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        DeletedBy = table.Column<int>(type: "int", nullable: true),
            //        DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_FollowUpMeeting", x => x.Id);
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "FollowUpMeeting");
        }
    }
}
