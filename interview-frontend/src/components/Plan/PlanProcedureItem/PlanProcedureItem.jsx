import React, { useState, useEffect } from "react";
import ReactSelect from "react-select";
import { useParams } from "react-router-dom";

import { addProcedureUsers, deleteProcedureUsers } from "../../../api/api";

const PlanProcedureItem = ({ procedure, users,procedureUsers }) => {
  let { id } = useParams(); 

  const currentProcedureId = procedure.procedureId;
  const [selectedUsers, setSelectedUsers] = useState(null);
  const [options, setOptions] = useState(users);

  useEffect(() => {
    if (procedureUsers) {
      console.log("inside");
      var filteredProcedures = procedureUsers.filter(
        (x) => x.procedureId === currentProcedureId
      );
      var filteredUsers = filteredProcedures.map((x) => x.userId);
      var procedureMappedUsers = users.filter((x) =>
        filteredUsers.includes(x.value)
      );
      var procedureUnmappedUsers = users.filter(
        (x) => !filteredUsers.includes(x.value)
      );
      setOptions(procedureUnmappedUsers);
      setSelectedUsers(procedureMappedUsers);
    }
  }, []);

  const addProcedureUsersParallel = async (e) => {
    const promises = e.map((x) =>
      addProcedureUsers(id, currentProcedureId, x.value)
    );
    await Promise.all(promises);
  };

  const handleAssignUserToProcedure = async (e) => {
    setSelectedUsers(e);
    await deleteProcedureUsers(id, currentProcedureId);
    addProcedureUsersParallel(e);
  };

  return (
    <div className="py-2">
      <div>{procedure.procedureTitle}</div>

      <ReactSelect
        className="mt-2"
        placeholder="Select User to Assign"
        isMulti={true}
        options={options}
        value={selectedUsers}
        onChange={(e) => handleAssignUserToProcedure(e)}
      />
    </div>
  );
};

export default PlanProcedureItem;
