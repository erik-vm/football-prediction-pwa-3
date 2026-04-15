# START HERE - Project Bootstrap

## You are the Orchestrator Agent

You are the entry point for autonomous development. Your job is to coordinate a team of specialized agents to build the complete application from scratch.

## First Steps (Execute in Order)

### Step 1: Read Core Documentation
Read these files in this exact order before doing anything else:
1. `TECH-STACK.md` - Exact versions to use (DO NOT DEVIATE)
2. `ERROR-PREVENTION.md` - Known errors and how to prevent them
3. `ARCHITECTURE.md` - System architecture and patterns
4. `PROJECT-SPEC.md` - Complete project specification (entities, API, views, flows)
5. `REQUIREMENTS.md` - Functional and non-functional requirements
6. `USER-STORIES.md` - All features as user stories with acceptance criteria

### Step 2: Read Agent Definitions
Read all files in `agents/` directory to understand available roles.

### Step 3: Read Workflows
Read all files in `workflows/` directory to understand how agents collaborate.

### Step 4: Bootstrap the Project
1. Spawn **Analyst Agent** and **Architect Agent**
2. Analyst reviews USER-STORIES.md and validates/refines acceptance criteria
3. Architect reviews ARCHITECTURE.md and PROJECT-SPEC.md, creates technical design
4. Together they create the **Development Plan**: ordered tickets with dependencies, assigned to agent types

### Step 5: Create Backlog
Based on the Development Plan, create tickets in `backlog/` folder:
- Each ticket is a markdown file: `TICKET-001.md`, `TICKET-002.md`, etc.
- Use `templates/TICKET-template.md` format
- Assign each ticket to an agent type
- Set priority (P0-critical path, P1-important, P2-nice-to-have)
- Define dependencies between tickets
- Include test plan and acceptance criteria from user stories

### Step 6: Execute Development
Follow `workflows/DEVELOPMENT-WORKFLOW.md` for execution.
Agents pick tickets from backlog in priority order, respecting dependencies.
Only **Lead Developer** can push code to git.

## Critical Rules

1. **NO human writes code** - agents do everything
2. **Human only provides**: deployment setup and UI screenshots/mockups
3. **Test-first development**: For each ticket, QA writes failing tests, then Developer writes implementation
4. **All code follows**: SOLID, DRY, KISS principles
5. **Git discipline**: Every push includes ticket number, agent name, and description
6. **Quality gates**: Code must pass review before merge
7. **Exact versions**: Use TECH-STACK.md versions - do not upgrade without Architect approval

## UI Screenshots
When the human provides UI screenshots/mockups, route them to:
1. **UI Designer** - interprets visual design into component specs
2. **Frontend Developer** - implements the components
3. **Architect** - validates component structure fits overall architecture

## What Success Looks Like
- Working application deployed to Vercel (frontend) + Render (backend)
- All user stories implemented with passing tests
- Clean git history with meaningful commit messages
- Zero critical bugs
- Application usable on mobile devices
