import { useState } from "react";
import { DialogComponent } from "@syncfusion/ej2-react-popups";
import { TextBoxComponent } from "@syncfusion/ej2-react-inputs";
import { ButtonComponent } from "@syncfusion/ej2-react-buttons";

function CreateOrganizationDialog({ visible, onClose, onCreate }) {
  const [form, setForm] = useState({
    code: "",
    name: "",
    tempEmail: "",
    mobileNo: "",
    phoneNo: "",
    website: "",
    streetAddress: ""
  });

  function submit() {
    if (!form.code.trim() || !form.name.trim()) {
      return alert("Code and Name are required");
    }
    onCreate(form);
  }

  return (
    <DialogComponent
      visible={visible}
      header="Add Organization"
      width="600px"
      isModal={true}
      showCloseIcon={true}
      close={onClose}
      footerTemplate={() => (
        <div className="d-flex justify-content-end gap-2">
          <ButtonComponent cssClass="e-flat" onClick={onClose}>
            Cancel
          </ButtonComponent>
          <ButtonComponent cssClass="e-primary" onClick={submit}>
            Add
          </ButtonComponent>
        </div>
      )}
    >
      <TextBoxComponent
        placeholder="Code"
        floatLabelType="Always"
        value={form.code}
        change={e => setForm({ ...form, code: e.value })}
      />
      <TextBoxComponent
        placeholder="Name"
        floatLabelType="Always"
        value={form.name}
        change={e => setForm({ ...form, name: e.value })}
      />
      <TextBoxComponent
        placeholder="Temp Email"
        floatLabelType="Always"
        value={form.tempEmail}
        change={e => setForm({ ...form, tempEmail: e.value })}
      />
      <TextBoxComponent
        placeholder="Mobile"
        floatLabelType="Always"
        value={form.mobileNo}
        change={e => setForm({ ...form, mobileNo: e.value })}
      />
      <TextBoxComponent
        placeholder="Phone"
        floatLabelType="Always"
        value={form.phoneNo}
        change={e => setForm({ ...form, phoneNo: e.value })}
      />
      <TextBoxComponent
        placeholder="Website"
        floatLabelType="Always"
        value={form.website}
        change={e => setForm({ ...form, website: e.value })}
      />
      <TextBoxComponent
        placeholder="Street Address"
        floatLabelType="Always"
        value={form.streetAddress}
        change={e => setForm({ ...form, streetAddress: e.value })}
      />
    </DialogComponent>
  );
}

export default CreateOrganizationDialog;
