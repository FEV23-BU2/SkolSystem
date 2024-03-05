import { useEffect, useState } from "react";
import "./App.css";

function Course({ id, name, description, groups }) {
  return (
    <>
      <h3>{name}</h3>
      <span>Name: </span>
      <span>{name}</span>
      <br />
      <span>Description: </span>
      <span>{description}</span>
    </>
  );
}

function LoggedInView({ setView }) {
  const [courseName, setCourseName] = useState("");
  const [courseDescription, setCourseDescription] = useState("");
  const [courses, setCourses] = useState([]);

  useEffect(() => {
    fetch("http://localhost:5250/api/course/")
      .then((res) => res.json())
      .then(setCourses);
  }, []);

  const createCourse = () => {
    fetch("http://localhost:5250/api/course/", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + localStorage.getItem("token"),
      },
      body: JSON.stringify({
        name: courseName,
        description: courseDescription,
      }),
    })
      .then((res) => {
        if (res.status === 409) {
          alert("A course with that name already exists.");
        } else if (res.status === 400) {
          alert("You must enter a proper name and description.");
        } else {
          res.json().then((course) => {
            setCourses([...courses, course]);
          });
        }
      })
      .catch((err) => {
        console.log(Object.keys(err));
        console.log(err);
      });
  };

  return (
    <div className="App">
      <button
        onClick={() => {
          localStorage.removeItem("token");
          setView("login");
        }}
      >
        Logout
      </button>
      <div>
        <h2>Create course</h2>
        <label>Name</label>
        <input
          value={courseName}
          onChange={(event) => setCourseName(event.target.value)}
        />
        <br />
        <label>Description</label>
        <input
          value={courseDescription}
          onChange={(event) => setCourseDescription(event.target.value)}
        />
        <br />
        <button onClick={createCourse}>Create</button>
      </div>
      <h2>Courses</h2>
      <ul>
        {courses.map((course) => (
          <Course
            id={course.id}
            name={course.name}
            description={course.description}
            groups={course.groups}
          />
        ))}
      </ul>
    </div>
  );
}

function LoginView({ setView }) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const login = () => {
    fetch("http://localhost:5250/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email,
        password,
      }),
    })
      .then((res) => res.json())
      .then((res) => {
        localStorage.setItem("token", res.accessToken);
        setView("main");
      });
  };

  return (
    <>
      <h1>Login</h1>
      <label>Email</label>
      <input value={email} onChange={(event) => setEmail(event.target.value)} />
      <br />
      <label>Password</label>
      <input
        value={password}
        onChange={(event) => setPassword(event.target.value)}
      />
      <br />
      <button onClick={login}>Login</button>
    </>
  );
}

function RegisterView() {}

function App() {
  const [view, setView] = useState(
    localStorage.getItem("token") !== null ? "main" : "login"
  );

  if (view === "login") {
    return <LoginView setView={setView} />;
  } else if (view === "register") {
    return <RegisterView setView={setView} />;
  } else {
    return <LoggedInView setView={setView} />;
  }
}

export default App;
