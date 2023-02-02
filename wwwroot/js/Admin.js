// ====================Admin===================
const uri = '/User';
let users = [];

function getUser() {
 fetch(`${uri}`, {
    method: 'GET',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    }
})
    .then(response => response.json())
    .then(data => _displayUsers(data))
    .catch(error => alert('Unable to get users.', error));
}
function addUser() {
    debugger
    const addNameTextbox = document.getElementById('add-name');
    const addPasswordTextbox = document.getElementById('add-password');

    const user = {
        isDone: false,
    
        name: addNameTextbox.value.trim(),
        password: addPasswordTextbox.value.trim()
   
    };
    fetch(uri, {
        
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            },
            body: JSON.stringify(user)
        })
        .then(response => response.json())
        .then(() => {
            getUser();
            addNameTextbox.value = '';
            addPasswordTextbox.value = '';
        })
        .catch(error => console.error('Unable to add user.', error));
}
function deleteUser(id) {
    debugger
    fetch(`${uri}/${id}`, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + token
            },
            body: JSON.stringify(id)
        })
        .then(() => getUser())
        .catch(error => console.error('Unable to delete user.', error));
}

// function displayTaskForm(id) {
//     const item = users.find(item => item.id === id);
//     document.getElementById('edit-id').value = item.id;
//     // document.getElementById('edit-name').value = item.name;
//     // document.getElementById('edit-isDone').checked = item.isDone;
//     document.getElementById('TaskForm').style.display = 'block';
// }

// function updateUser() {
//     const itemId = document.getElementById('edit-id').value;
//     const item = {
//         id: parseInt(itemId, 10)
//     };


//     fetch(`${uri}/${itemId}`, {
//             method: 'PUT',
//             headers: {
//                 'Accept': 'application/json',
//                 'Content-Type': 'application/json'
//             },
//             body: JSON.stringify(item)
//         })
//         .then(() => getItems())
//         .catch(error => console.error('Unable to update user.', error));

//     closeInput();
//     return false;
// }

function closeInput() {
    document.getElementById('TaskForm').style.display = 'none';
}
    
function _displayCount(itemCount) {
    const name = (itemCount <= 1) ? 'user' : 'users';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayUsers(data) {
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';

   // _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(user => {
        
        let IsAdminCheckbox = document.createElement('input');
        IsAdminCheckbox.type = 'checkbox';
        IsAdminCheckbox.disabled = true;
        IsAdminCheckbox.checked = user.IsAdmin;
        
        // let editButton = button.cloneNode(false);
        // editButton.innerText = 'Edit';
        // editButton.setAttribute('onclick', `displayTaskForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteUser(${user.id})`);
        
        let tr = tBody.insertRow();
       
        let td1 = tr.insertCell(0);
       
        td1.appendChild(IsAdminCheckbox);
       
        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(user.name);
        td2.appendChild(textNode);
        
        // let td3 = tr.insertCell(2);
        // td3.appendChild(editButton);
        
        let td4 = tr.insertCell(2);
        td4.appendChild(deleteButton);
       
    });

    users = data;
}
