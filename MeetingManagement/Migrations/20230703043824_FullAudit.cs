using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingManagement.Migrations
{
    /// <inheritdoc />
    public partial class FullAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Participants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Participants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Participants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Participants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Participants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Participants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Participants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Meetings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Meetings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Meetings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "Meetings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Meetings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Meetings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Meetings",
                type: "int",
                nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "CreatedAt",
            //    table: "ExternalUsers",
            //    type: "datetime2",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            //migrationBuilder.AddColumn<int>(
            //    name: "CreatedBy",
            //    table: "ExternalUsers",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "DeletedAt",
            //    table: "ExternalUsers",
            //    type: "datetime2",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "DeletedBy",
            //    table: "ExternalUsers",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsActive",
            //    table: "ExternalUsers",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "UpdatedAt",
            //    table: "ExternalUsers",
            //    type: "datetime2",
            //    nullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "UpdatedBy",
            //    table: "ExternalUsers",
            //    type: "int",
            //    nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BookedMeetingRooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "BookedMeetingRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "BookedMeetingRooms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "BookedMeetingRooms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BookedMeetingRooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "BookedMeetingRooms",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "BookedMeetingRooms",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ExternalUsers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ExternalUsers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ExternalUsers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "ExternalUsers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ExternalUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ExternalUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ExternalUsers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BookedMeetingRooms");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BookedMeetingRooms");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "BookedMeetingRooms");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BookedMeetingRooms");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BookedMeetingRooms");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BookedMeetingRooms");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "BookedMeetingRooms");
        }
    }
}
