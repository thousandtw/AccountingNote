<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxAccountingNote7308.aspx.cs" Inherits="AccountingNote7308.AjaxAccountingNote7308" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>使用 AJAX 更新 AccountingNote</title>
    <script src="Handlers/jQuery-min-360.js"></script>
    <script>
        $(function () {
            $("#btnSave").click(function () {
                var id = $("#hfID").val();
                var actType = $("#ddIActType").val();
                var amount = $("#txtAmount").val();
                var caption = $("#txtCaption").val();
                var desc = $("#txtDesc").val();

                //分辨新增修改
                if (id) {
                    $.ajax({
                        url: "http://localhost:52096/Handlers/AccountingNoteHandler.ashx?ActionName=update",                 //資料檔名(找)
                        type: "POST",
                        data: {
                            "ID": id,
                            "Caption": caption,
                            "Amount": amount,
                            "ActType": actType,
                            "Body": desc
                        },
                        success: function (result) {    //建立result變數存入資料檔內容,並放入function中
                            alert("更新成功")
                        }
                    });
                }
                else {
                    $.ajax({
                        url: "http://localhost:52096/Handlers/AccountingNoteHandler.ashx?ActionName=create",
                        type: "POST",
                        data: {
                            "Caption": caption,
                            "Amount": amount,
                            "ActType": actType,
                            "Body": desc
                        },
                        success: function (result) {
                            alert("新增成功")
                        }
                    });
                }

            });


            //$("#btnRead").click(function () {
            //    $.ajax({
            //        url: "http://localhost:52096/Handlers/AccountingNoteHandler.ashx?ActionName=query",      //資料檔名(找)
            //        type: "POST",
            //        data: {
            //            "ID": 1,
            //        },
            //        success: function (result) {
            //            $("#hfID").val(result["ID"]);
            //            $("#ddlActType").val(result["ActType"]);
            //            $("#txtAmount").val(result["Amount"]);
            //            $("#txtCaption").val(result["Caption"]);
            //            $("#txtDesc").val(result["Body"]);
            //        }
            //    });
            //});

            $(document).on("click", ".btnReadData", function () {
                var td = $(this).closest("td");   //找到btnReadData的td
                var hf = td.find("input.hfRowID"); //找到td裡的hfRowID

                var rowID = hf.val();   //把hfRowID放入rowID

                $.ajax({
                    url: "http://localhost:52096/Handlers/AccountingNoteHandler.ashx?ActionName=query",      //資料檔名(找)
                    type: "POST",
                    data: {
                        "ID": rowID,
                    },                                             //把取得單列資料輸出在上方的格內
                    success: function (result) {
                        $("#hfID").val(result["ID"]);
                        $("#ddlActType").val(result["ActType"]);
                        $("#txtAmount").val(result["Amount"]);                 
                        $("#txtCaption").val(result["Caption"]);
                        $("#txtDesc").val(result["Body"]);

                        $("#divEditor").show(300);
                    }
                });
            });

            $("#btnAdd").click(function () {         //開啟
                $("#hfID").val('');
                $("#ddlActType").val('');
                $("#txtAmount").val('');
                $("#txtCaption").val('');
                $("#txtDesc").val('');

                $("#divEditor").show(300);           //300為速量
            });

            $("#btnCancle").click(function () {      //收合
                $("#hfID").val('');
                $("#ddlActType").val('');
                $("#txtAmount").val('');
                $("#txtCaption").val('');
                $("#txtDesc").val('');

                $("#divEditor").hide(300);
            });

            $("#divEditor").hide();

            //發ajax取得表格內容
            $.ajax({
                url: "http://localhost:52096/Handlers/AccountingNoteHandler.ashx?ActionName=list",
                type: "GET",
                data: {},
                success: function (result) {
                    var table = '<table border="1">';
                    table += '<tr> <th>Caption</th> <th>Amount</th> <th>ActType</th> <th>CreateDate</th> <th>Act</th> </tr>';
                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        var htmlText =
                            `<tr> 
                                <td>${obj.Caption}</td>
                                <td>${obj.Amount}</td>
                                <td>${obj.ActType}</td>
                                <td>${obj.CreateDate}</td>
                                <td>
                                    <input type="hidden" class="hfRowID" value="${obj.ID}" />
                                    <button type="button" class="btnReadData">
                                        <img src="Images/1046px-Search_Noun_project_15028.svg.png" width="20" height="20" />
                                    </button>
                                </td>
                            </tr>`;
                        table += htmlText;
                    }
                    table += "</table>";
                    $("#divAccountingList").append(table);
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div id="divEditor">
            <input type="hidden" id="hfID" />
            <%-- <button type="button" id="btnRead">Read Data</button>--%>
            <table>
                <tr>
                    <td>類別:
                   <select id="ddIActType">                         <%--//純前端的下拉選單--%>
                       <option value="0">支出</option>                    
                       <option value="1">收入</option>
                   </select>
                        <br />
                        金額:
                    <input type="number" id="txtAmount" /><br />   
                        標題:                                           <%--//純前端的文字--%>
                   <input type="text" id="txtCaption" /><br />
                        說明:            <%--幾字的高/幾字的寬--%>
                    <textarea id="txtDesc" rows="5" cols="60"></textarea>
                        <br />
                        <button type="button" id="btnSave">SAVE</button>
                        <button type="button" id="btnCancle">CANCLE</button>
                    </td>
                </tr>
            </table>
        </div>

        <button type="button" id="btnAdd">ADD</button>
        <div id="divAccountingList"></div>
    </form>
</body>
</html>
