<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DataTablesServerSide._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/DataTables-1.10.4/css/jquery.dataTables.css" rel="stylesheet" />
    <script src="Scripts/DataTables-1.10.4/jquery.dataTables.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $.ajaxSetup({
                cache: false
            });

            function showDetails() {
                alert("showing some details");
            }

            var table = $('#tblData').DataTable({
                "filter": false,
                "pagingType": "simple_numbers",
                "orderClasses": false,
                "order": [[0, "asc"]],
                "info": false,
                "scrollY": "450px",
                "scrollCollapse": true,
                "bProcessing": true,
                "bServerSide": true,
                "sAjaxSource": "WebService.asmx/GetTableData",
                "fnServerData": function (sSource, aoData, fnCallback) {
                    aoData.push({ "name": "roleId", "value": "admin" });
                    $.ajax({
                        "dataType": 'json',
                        "contentType": "application/json; charset=utf-8",
                        "type": "GET",
                        "url": sSource,
                        "data": aoData,
                        "success": function (msg) {
                            var json = jQuery.parseJSON(msg.d);
                            fnCallback(json);
                            $("#tblData").show();
                        },
                        error: function (xhr, textStatus, error) {
                            if (typeof console == "object") {
                                console.log(xhr.status + "," + xhr.responseText + "," + textStatus + "," + error);
                            }
                        }
                    });
                },
                fnDrawCallback: function () {
                    $('.image-details').bind("click", showDetails);
                }
            });
        })
    </script>
    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <table id="tblData" class="hover">  
           <thead>
              <tr class="gridStyle">
                 <th>UserId</th>
                 <th>Name</th>
                 <th>Address</th>
                 <th>Age</th>
                 <th>View Details</th>
              </tr>
           </thead>
           <tbody></tbody>
        </table>  
    </div>

</asp:Content>
