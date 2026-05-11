import { GridComponent, ColumnsDirective, ColumnDirective,Page, Sort, Inject, Resize, Reorder, ColumnMenu,Toolbar } from "@syncfusion/ej2-react-grids";
function OrganizationGrid({organizations, onToggle, onEdit, onDetails, onVerifyEmail }){
    const statusTemplate = (props) => (
    <span style={{ fontWeight: 600, color: props.status ? "green" : "red" }}>
      {props.status ? "Active" : "Inactive"}
    </span>
  ); 

    const actionTemplate = (props) => (
    <div className="d-flex gap-2 justify-content-center">
      <button
        className="e-btn e-small e-info"
        onClick={() => onDetails(props.organizationId)}
      >
        Details
      </button>
      <button
        className="e-btn e-small e-primary"
        onClick={() => onEdit(props.organizationId)}
      >
        Edit
      </button>
      {onVerifyEmail && (
        <button
          className="e-btn e-small e-warning"
          onClick={() => onVerifyEmail(props)}
        >
          Verify Email
        </button>
      )}
      <button
        className={`e-btn e-small ${props.status ? "e-danger" : "e-success"}`}
        onClick={() => onToggle(props)}
      >
        {props.status ? "Deactivate" : "Activate"}
      </button>
    </div>
  );

    const mobileValueAccessor = (field, data) => {
    return data.mobileNo ? `+977 ${data.mobileNo}` : "-";
  }; 

    return (
    <GridComponent
      dataSource={organizations}
      allowPaging={true}
      allowSorting={true}
      allowResizing={true}
      allowReordering={true}
      showColumnMenu={true}
      allowTextWrap={true}
      toolbar={["ColumnChooser"]}
      pageSettings={{ pageSize: 10 }}
      height={420}
      autoFitColumns={true}
    >
      <ColumnsDirective>
        <ColumnDirective
          field="code"
          headerText="Code"
          width="120"
          textAlign="Left"
          customAttributes={{ class: "fw-semibold" }}
        />
        <ColumnDirective
          field="name"
          headerText="Name"
          width="150"
          textAlign="Left"
          customAttributes={{ class: "fw-semibold" }}
        />
        <ColumnDirective
          field="email"
          headerText="Email"
          width="220"
          textAlign="Left"
        />
        <ColumnDirective
          field="mobileNo"
          headerText="Mobile"
          width="120"
          valueAccessor={mobileValueAccessor}
          textAlign="Center"
        />
        <ColumnDirective
          headerText="Status"
          width="100"
          template={statusTemplate}
          textAlign="Center"
        />
        <ColumnDirective
          headerText="Actions"
          width="300"
          template={actionTemplate}
          allowSorting={false}
          allowFiltering={false}
          textAlign="Center"
        />
      </ColumnsDirective>

      <Inject services={[Page, Sort, Resize, Reorder, ColumnMenu, Toolbar]} />
    </GridComponent>
  );
}

export default OrganizationGrid;