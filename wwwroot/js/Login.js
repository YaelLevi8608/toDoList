const url='/user'
const login=() =>{
  const name = document.getElementById('name');
  const password = document.getElementById('password');
  const user = {
  Name: name.value.trim(),
  Password: password.value.trim()
  };

  fetch(`${url}/Login`,{
    method: 'POST', 
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
  },
    body:JSON.stringify(user)
  })
    .then(response => response.json())
    .then(data => {
      name.value="";
      password.value="";
      localStorage.setItem("token",data)
      location.href="/UserTask.html";
  })
  .catch(error => {
    name.value="";
    password.value="";
    console.log(alert("user is not found", error));
  });
  }



 