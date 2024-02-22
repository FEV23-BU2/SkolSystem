using Microsoft.AspNetCore.Mvc;

namespace Backend;

public class GroupDto
{
    public Guid Id { get; set; }

    public GroupDto(Group group)
    {
        this.Id = group.Id;
    }
}

[ApiController]
[Route("api/group")]
public class GroupController : ControllerBase
{
    GroupService groupService;

    public GroupController(GroupService groupService)
    {
        this.groupService = groupService;
    }

    [HttpPost]
    public GroupDto CreateGroup()
    {
        Group group = groupService.CreateGroup();
        return new GroupDto(group);
    }

    [HttpGet]
    public List<GroupDto> GetAllGroups()
    {
        return groupService.GetAll().Select(group => new GroupDto(group)).ToList();
    }

    [HttpPut]
    public IActionResult AddGroupToCourse([FromQuery] Guid courseId, [FromQuery] Guid groupId)
    {
        groupService.AddGroupToCourse(courseId, groupId);
        return Ok();
    }
}
