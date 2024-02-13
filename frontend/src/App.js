import { useEffect, useState } from 'react';
import './App.css';

function Course({ id, name, description, groups }) {
  return <>
    <h3>{name}</h3>
    <span>Name: </span>
    <span>{name}</span>
    <br />
    <span>Description: </span>
    <span>{description}</span>
  </>;
}

function App() {
  const [courseName, setCourseName] = useState('');
  const [courseDescription, setCourseDescription] = useState('');
  const [courses, setCourses] = useState([]);

  useEffect(() => {
    fetch("http://localhost:5250/api/course/")
      .then(res => res.json())
      .then(setCourses);
  }, []);

  const createCourse = () => {
    fetch("http://localhost:5250/api/course/", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        name: courseName,
        description: courseDescription
      })
    })
    .then(res => res.json())
    .then(course => {
      setCourses([...courses, course]);
    })
  };

  console.log(courses);

  return (
    <div className="App">
      <div>
        <h2>Create course</h2>
        <label>Name</label>
        <input value={courseName} onChange={event => setCourseName(event.target.value)}/>
        <br />
        <label>Description</label>
        <input value={courseDescription} onChange={event => setCourseDescription(event.target.value)}/>
        <br />
        <button onClick={createCourse}>Create</button>
      </div>
      <h2>Courses</h2>
      <ul>
        {courses.map(course => <Course id={course.id} 
          name={course.name} 
          description={course.description} 
          groups={course.groups}
        />)}
      </ul>
    </div>
  );
}

export default App;
