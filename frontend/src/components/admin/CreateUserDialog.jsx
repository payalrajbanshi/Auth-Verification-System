import { useState } from "react";
import { DialogComponent } from "@syncfusion/ej2-react-popups";
import { TextBoxComponent } from "@syncfusion/ej2-react-inputs";
import { ButtonComponent } from "@syncfusion/ej2-react-buttons";

function CreateUserDialog({ visible, onClose, onCreate }) {
  const [form, setForm] = useState({
    name: "",
    username: "",
    mobileNo: "",
    tempEmail: "",
    password: "",
    userType: "User"
  });

  function submit() {
    if (!form.name.trim() || !form.username.trim() ) {
      return alert("Name, username and password are  missing");
    }
    onCreate(form);

  }

  return (
    <DialogComponent
      visible={visible}
      header="Add User"
      width="600px"
      isModal={true}
      showCloseIcon={true}
      close={onClose}
    footerTemplate={() => (
        <div>
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
        placeholder="Name"
        floatLabelType="Always"
        value={form.name}
        change={e => setForm({ ...form, name: e.value })}
      />
      <TextBoxComponent
        placeholder="Username"
        floatLabelType="Always"
        value={form.username}
        change={e => setForm({ ...form, username: e.value })}
      />
      <TextBoxComponent
        placeholder="Mobile"
        floatLabelType="Always"
        value={form.mobileNo}
        change={e => setForm({ ...form, mobileNo: e.value })}
      />
      <TextBoxComponent
        placeholder="Email"
        floatLabelType="Always"
        value={form.tempEmail}
        change={e => setForm({ ...form, tempEmail: e.value })}
      />
      <TextBoxComponent
        type="password"
        floatLabelType="Always"
        placeholder="Password"
        value={form.password}
        change={e => setForm({ ...form, password: e.value })}
      />

      <select
        className="e-input"
        
        value={form.userType}
        onChange={e => setForm({ ...form, userType: e.target.value })}
      >
        <option value="">Select User Type</option>
        <option value="Reseller">Reseller</option>
        <option value="Organization">Organization</option>
        <option value="Branch">Branch</option>
      </select>
    </DialogComponent>
  );
}

export default CreateUserDialog;
