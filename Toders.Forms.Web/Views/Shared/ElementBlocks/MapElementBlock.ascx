<%@ Control Language="C#" Inherits="ViewUserControl<Toders.Forms.Models.Forms.MapElementBlock>" %>
<%@ Import Namespace="EPiServer.Forms.Helpers" %>

<%
    var formElement = Model.FormElement; 
    var labelText = Model.Label;
%>

<div class="Form__Element FormTextbox" data-epiforms-element-name="<%:formElement.Code %>">
	<label for="<%:formElement.Guid%>" class="Form__Element__Caption"><%:labelText%></label>
	<input name="<%:formElement.Code%>" id="<%:formElement.Guid%>" type="text" class="FormTextbox__Input" <%: Html.Raw(formElement.AttributesString) %> />
	<div id="us2" style="width: 500px; height: 400px;"></div>
	<% if (EPiServer.Editor.PageEditing.PageIsInEditMode == false)
	   { %>

	<script>
		$('#us2').locationpicker({
			location: { latitude: 	59.32536, longitude: 18.071197 },
			radius: 0,
			zoom: 12,
			inputBinding: {
				locationNameInput: $('#<%: formElement.Guid %>')
			}
		});
	</script>
	<% } %>
</div>
