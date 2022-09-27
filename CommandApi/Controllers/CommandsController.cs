using AutoMapper;
using CommandApi.App.Data;
using CommandApi.App.Dtos;
using CommandApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommandApi.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo commandRepo, IMapper mapper)
        {
            this._commandRepo = commandRepo;
            this._mapper = mapper;
        }

        // GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandList = _commandRepo.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandList));
        }

        // GET api/commands/{id}
        [HttpGet("{id}")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var command = _commandRepo.GetCommandById(id);

            if (command != null)
                return Ok(_mapper.Map<CommandReadDto>(command));

            return NotFound();

        }

        // POST api/commands
        [HttpPost]
        //public async Task<IActionResult> CreateCommand(CommandCreateDto commandCreateDto)
        public async Task<IActionResult> CreateCommand(
            [Bind("HowTo")] CommandCreateDto commandCreateDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var command = _mapper.Map<Command>(commandCreateDto);
                    _commandRepo.CreateCommand(command);
                    _commandRepo.Save();

                    var commandReadDto = _mapper.Map<CommandReadDto>(command);

                    // Only return the DTO, not the original model 
                    return Ok(_mapper.Map<CommandReadDto>(command));

                    // return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", ex.Message + " - Unable to save changes.");
                throw;
            }
            return BadRequest();
        }

        // PUT api/commands/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCommand(int? id, CommandUpdateDto commandUpdateDto)
        {
            if (id == null)
                return NotFound(); 

            var commandModel = _commandRepo.GetCommandById(id);

            _mapper.Map(commandUpdateDto, commandModel);
            _commandRepo.UpdateCommand(commandModel);
            _commandRepo.Save();

            return NoContent();
        }

        // PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> EditCommand(int? id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModel = _commandRepo.GetCommandById(id);
            if (commandModel == null)
                return NotFound();

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModel);
            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(commandToPatch, commandModel);
            _commandRepo.UpdateCommand(commandModel);
            _commandRepo.Save();

            return NoContent();
        }

        // DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModel = _commandRepo.GetCommandById(id);
            if (commandModel == null)
                return NotFound();

            _commandRepo.DeleteCommand(commandModel);
            _commandRepo.Save();

            return NoContent(); 
        }
    }
}
